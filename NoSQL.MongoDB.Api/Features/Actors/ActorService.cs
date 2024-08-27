using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using NoSQL.MongoDB.Api.Database;
using NoSQL.MongoDB.Api.Interfaces.Services;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Features.Actors;

/// <summary>
/// Service for managing actor operations.
/// </summary>
public class ActorService(DbContext dbContext) : IActorService
{
    private readonly IMongoCollection<Actor> _actors = dbContext.Actors;

    /// <inheritdoc/>
    public async Task<List<Actor>> GetAsync()
    {
        return await _actors.Find(_ => true)
            .Limit(10)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Actor?> GetByIdAsync(string id)
    {
        return await _actors.Find(actor => actor.Id == id)
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<Actor> CreateAsync(Actor newActor)
    {
        await _actors.InsertOneAsync(newActor);
        return newActor;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(string id, Actor updatedActor)
    {
        await _actors.ReplaceOneAsync(actor => actor.Id == id, updatedActor);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(string id)
    {
        await _actors.DeleteOneAsync(actor => actor.Id == id);
    }
}