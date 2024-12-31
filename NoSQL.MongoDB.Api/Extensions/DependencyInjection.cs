using Microsoft.Extensions.DependencyInjection;
using NoSQL.MongoDB.Api.Configurations;
using NoSQL.MongoDB.Api.Database;

namespace NoSQL.MongoDB.Api.Extensions;

/// <summary>
/// DI extension class.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// MongoDB DI extension method
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void AddMongoDb(this IServiceCollection services)
    {
        services.AddOptions<MongoDbSettings>()
            .BindConfiguration(MongoDbSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddSingleton<DbContext>();
    }
}