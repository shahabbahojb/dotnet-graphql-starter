using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//builder.Services.AddScoped<IContextSeed, ContextSeed>();

builder.AddGraphQL().AddTypes();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);