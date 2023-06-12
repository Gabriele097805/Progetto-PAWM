using Microsoft.EntityFrameworkCore;

namespace ToDoList.Database;

public static class Migrations
{
    /// <summary>
    /// Adds the required services for database migrations.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collections.</returns>
    public static IServiceCollection AddMigrationsRequiredDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMigrationsProvider, MigrationsProvider>();
        return services;
    }

    /// <summary>
    /// Applies the migrations automatically at startup.
    /// </summary>
    /// <param name="app">The app host.</param>
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope()!;

        var migrationsService = scope.ServiceProvider.GetRequiredService<IMigrationsProvider>();
        migrationsService.Migrate();

        return app;
    }
}

public interface IMigrationsProvider
{
    void Migrate();
}

public class MigrationsProvider : IMigrationsProvider
{
    private readonly AppDbContext m_dbContext;
    private readonly ILogger<MigrationsProvider> m_logger;

    public MigrationsProvider(AppDbContext dbContext, ILogger<MigrationsProvider> logger)
    {
        m_dbContext = dbContext;
        m_logger = logger;
    }

    public void Migrate()
    {
        m_logger.LogInformation("Starting database migrations.");

        try
        {
            m_dbContext.Database.Migrate();
            m_logger.LogInformation("Database migrations applied.");
        }
        catch (Exception ex)
        {
            m_logger.LogError(ex, "Error while applying database migrations.");
            throw;
        }
    }
}
