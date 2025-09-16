using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyApp.Infrastructure.Data;

public class DesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        // Dev-time connection:
        builder.UseNpgsql("Host=localhost;Port=5432;Database=myapp;Username=postgres;Password=postgres",
            o => o.MigrationsAssembly("MyApp.Migrations"));
        return new ApplicationDbContext(builder.Options);
    }
}