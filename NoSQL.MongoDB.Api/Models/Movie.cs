using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoSQL.MongoDB.Api.Models;

// https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/serialization/poco/
// https://www.mongodb.com/docs/manual/reference/limits/#naming-restrictions
public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Plot { get; set; }
    
    public List<string> Genres { get; set; }
    
    public int Runtime { get; set; }
    
    public List<string> Cast { get; set; }
    
    public string Poster { get; set; }
    
    public string Title { get; set; }
    
    [BsonElement("fullplot")]
    public string FullPlot { get; set; }
    
    public List<string> Languages { get; set; }
    
    public DateTime Released { get; set; }
    
    public List<string> Directors { get; set; }
    
    public List<string>? Writers { get; set; }
    
    public string? Rated { get; set; }
    
    public Awards Awards { get; set; }
    
    [BsonElement("lastupdated")]
    public string LastUpdated { get; set; }
    
    public int Year { get; set; }
    
    public Imdb? Imdb { get; set; }
    
    public List<string> Countries { get; set; }
    
    public string Type { get; set; }
    
    [BsonElement("num_mflix_comments")]
    public int NumMflixComments { get; set; }
}

public class Awards
{
    public int Wins { get; set; }
    
    public int Nominations { get; set; }
    
    public string Text { get; set; }
}

// Imd stands for Internet Movie Database
public class Imdb
{
    public double Rating { get; set; }
    
    public int Votes { get; set; }
    
    // Rename field Id to UnObjectId because of the conflict with the Id property:https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/serialization/poco/#identify-id-property
    [BsonElement("id")]
    [JsonPropertyName("id")]
    public int UnObjectId { get; set; }
}