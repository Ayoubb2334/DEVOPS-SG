using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = BuildConnectionString(configuration);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static string BuildConnectionString(IConfiguration configuration)
    {
        // Priorité aux variables d'environnement (utilisées en conteneur Docker/Linux),
        // avec repli sur appsettings (utile en développement local sous Windows).
        var host = Environment.GetEnvironmentVariable("PG_HOST") ?? configuration["DataConnection:Host"] ?? "localhost";
        var database = Environment.GetEnvironmentVariable("PG_DATABASE") ?? configuration["DataConnection:Database"] ?? "smartphonedb";
        var username = Environment.GetEnvironmentVariable("PG_USERNAME") ?? configuration["DataConnection:Username"] ?? "postgres";
        var password = Environment.GetEnvironmentVariable("PG_PASSWORD") ?? configuration["DataConnection:Password"] ?? "postgres";
        var port = Environment.GetEnvironmentVariable("PG_PORT") ?? configuration["DataConnection:Port"] ?? "5432";

        return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
    }
}
