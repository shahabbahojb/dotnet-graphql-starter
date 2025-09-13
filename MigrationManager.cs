using Demo.Gql.Types;
using Microsoft.EntityFrameworkCore;

namespace Demo.Gql;

public static class MigrationExtensions
{
    public static IServiceCollection AddMigration<TContext, TSeeder>(
        this IServiceCollection services)
        where TContext : DbContext
        where TSeeder : class
    {
        // Add the seeder to DI
        services.AddTransient<TSeeder>();

        // Build a temporary provider so we can run migration + seeding
        using var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        // 1. Apply migrations
        context.Database.Migrate();

        // 2. Run seeder
        var seeder = scope.ServiceProvider.GetRequiredService<TSeeder>();
        var seedMethod = typeof(TSeeder).GetMethod("SeedAsync");

        if (seedMethod != null)
        {
            var task = (Task)seedMethod.Invoke(seeder, new object[] { context })!;
            task.GetAwaiter().GetResult();
        }

        return services;
    }
}

public interface IContextSeed
{
    Task SeedAsync(AppDbContext context);
}

public class ContextSeed : IContextSeed
{
    public async Task SeedAsync(AppDbContext context)
    {
        if (!context.Books.Any())
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "The Great Gatsby",
                    Author = new Author { Name = "F. Scott Fitzgerald" }
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Author = new Author { Name = "Harper Lee" }
                },
                new Book
                {
                    Title = "1984",
                    Author = new Author { Name = "George Orwell" }
                }
            );

            await context.SaveChangesAsync();
        }
    }
}