using System.ComponentModel.DataAnnotations;

namespace NoSQL.MongoDB.Api.Configurations;

public class MongoDbSettings
{
    [Required]
    public string ConnectionString { get; init; }
    
    [Required]
    public string DatabaseName { get; init; }
}