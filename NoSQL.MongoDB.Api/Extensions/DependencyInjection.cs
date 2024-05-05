using Microsoft.Extensions.DependencyInjection;
using NoSQL.MongoDB.Api.Configurations;
using NoSQL.MongoDB.Api.Database;

namespace NoSQL.MongoDB.Api.Extensions;

public static class DependencyInjection
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        services.AddOptions<MongoDbSettings>()
            .BindConfiguration(SectionName.MongoDbSettings)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddSingleton<DbContext>();
    }
    
}