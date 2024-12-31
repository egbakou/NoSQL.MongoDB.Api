using System.ComponentModel.DataAnnotations;

namespace NoSQL.MongoDB.Api.Configurations;

/// <summary>
/// MongoDB settings
/// </summary>
public class MongoDbSettings
{
    /// <summary>
    /// MongoDB section name in appsettings.json
    /// </summary>
    public const string SectionName = "MongoDB";
    
    /// <summary>
    /// MongoDB connection string
    /// </summary>
    [Required]
    public string ConnectionString { get; init; }

    /// <summary>
    /// MongoDB database name
    /// </summary>
    [Required]
    public string DatabaseName { get; init; }
}