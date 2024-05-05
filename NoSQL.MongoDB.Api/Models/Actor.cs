using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSQL.MongoDB.Api.Models;

// The collection "actors" does not exist in the database, it will be automatically created
public class Actor
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Name { get; set; }
    
    public List<string>? Aliases { get; set; }
    
    public string? Biography { get; set; }
    
    public string? Image { get; set; }
    
    public List<string> Movies { get; set; }
}