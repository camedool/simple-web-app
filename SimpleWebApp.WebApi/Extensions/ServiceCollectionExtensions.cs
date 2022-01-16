using Microsoft.EntityFrameworkCore;
using SimpleWebApp.WebApi.Data;
using SimpleWebApp.WebApi.Repositories;
using SimpleWebApp.WebApi.Services;
using SimpleWebApp.WebApi.Settings;

namespace SimpleWebApp.WebApi.Extensions;

internal static class ServiceCollectionExtensions
{
    private static readonly string DbRelativeDirectory = "Db";

    /// <summary>
    /// Extension method to add a dbContext for SQLite.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configuration"></param>
    /// <param name="projectDirectory"></param>
    /// <returns></returns>
    internal static IServiceCollection AddSqlite(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = GetSqliteConnectionString(configuration);

        serviceCollection.AddDbContext<AppDbContext>(
            opt => opt.UseSqlite(Path.Combine(connectionString)));

        return serviceCollection;
    }

    internal static IServiceCollection AddAppServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IInventoryService, InventoryService>();
        serviceCollection.AddScoped<IWarehouseService, WarehouseService>();
        serviceCollection.AddScoped<IItemService, ItemService>();

        return serviceCollection;
    }

    internal static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IInventoryRepository, InventoryRepository>();
        serviceCollection.AddScoped<IWarehouseRepository, WarehouseRepository>();
        serviceCollection.AddScoped<IItemRepository, ItemRepository>();

        return serviceCollection;
    }

    /// <summary>
    /// Puts together connectionString for SQLite: db directory && fileName provided in appsettings
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>SQLite connection string.</returns>
    private static string GetSqliteConnectionString(IConfiguration configuration)
    {
        var fileName = configuration
            .GetSection(SqliteSetting.AppSettingsPath).Get<SqliteSetting>().FileName;
        
        var path = Path.Combine(DbRelativeDirectory, fileName);
        EnsureDbFileExits(path);

        return $"Data Source={Path.Combine(DbRelativeDirectory, fileName)}";
    }

    private static void EnsureDbFileExits(string path)
    {   // make sure directory exists
        if (!Directory.Exists(DbRelativeDirectory))
        {
            Directory.CreateDirectory(DbRelativeDirectory);
        }

        // make sure file exists
        if (!File.Exists(path))
        {
            File.Create(path);
        }
    }
}
