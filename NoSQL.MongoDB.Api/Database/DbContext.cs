using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NoSQL.MongoDB.Api.Configurations;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Database;

/// <summary>
/// Represents the database context for MongoDB operations.
/// </summary>
public class DbContext
{
    private readonly IMongoDatabase _database;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DbContext"/> class.
    /// </summary>
    /// <param name="settings">The MongoDB settings</param>
    public DbContext(IOptions<MongoDbSettings> settings)
    {
        var settingsValue = settings.Value;
        
        var clientSettings = MongoClientSettings.FromConnectionString(settingsValue.ConnectionString);
        //clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        
        var client = new MongoClient(clientSettings);
        _database = client.GetDatabase(settingsValue.DatabaseName);
    }
    
    /// <summary>
    /// Gets the movies collection.
    /// </summary>
    public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("movies");
    
    /// <summary>
    /// Gets the actors collection.
    /// </summary>
    public IMongoCollection<Actor> Actors => _database.GetCollection<Actor>("actors");
}