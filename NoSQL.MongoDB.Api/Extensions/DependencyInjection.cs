using Microsoft.Extensions.DependencyInjection;
using NoSQL.MongoDB.Api.Configurations;
using NoSQL.MongoDB.Api.Database;

namespace NoSQL.MongoDB.Api.Extensions;

public static class DependencyInjection
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        // Bind hierarchical configuration
        //var mongoSettings = configuration.GetValue<MongoDbSettings>("MongoDB");

        // Options Pattern with Validation
        // Options MongoDbSettings can be injection using:
        // - IOptions<MongoDbSettings>: does not support Reading of configuration data after the app has started.
        // - IOptionsSnapshot<MongoDbSettings>: supports Reading of configuration data after the app has started. value is computed on each request.
        // - IOptionsMonitor<MongoDbSettings>: supports Reading of configuration data after the app has started. update/reload is automatic. change notification
        services.AddOptions<MongoDbSettings>()
            .BindConfiguration(SectionName.MongoDbSettings)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        // services.AddOptions<> allows services.PostConfigure<> to be used to further configure the options after the options have been bound and validated.

        // Options Pattern without Validation
        //services.Configure<MongoDbSettings>(configuration.GetSection(SectionName.MongoDbSettings));
        // If you don't want to specify the section name, you can use the following. MongoDbSettingsSetup should inherit IConfigureOptions<MongoDbSettings>
        //services.ConfigureOptions<MongoDbSettingsSetup>();


        services.AddSingleton<DbContext>();
    }
}