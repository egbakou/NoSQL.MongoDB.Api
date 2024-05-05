using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NoSQL.MongoDB.Api.Configurations;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Database;

public class DbContext
{
    private readonly IMongoDatabase _database;

    public DbContext(IOptions<MongoDbSettings> settings)
    {
        var settingsValue = settings.Value;
        
        var clientSettings = MongoClientSettings.FromConnectionString(settingsValue.ConnectionString);
        //clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        
        var client = new MongoClient(clientSettings);
        _database = client.GetDatabase(settingsValue.DatabaseName);
    }
    
    public IMongoCollection<Movie> Movies => _database.GetCollection<Movie>("movies");
    public IMongoCollection<Actor> Actors => _database.GetCollection<Actor>("actors");
}