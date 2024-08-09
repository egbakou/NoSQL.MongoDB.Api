using System.Collections.Generic;
using System.Threading.Tasks;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Interfaces.Services;

/// <summary>
/// Interface for actor service operations.
/// </summary>
public interface IActorService
{
    /// <summary>
    /// Retrieves a list of actors.
    /// </summary>
    Task<List<Actor>> GetAsync();
    
    /// <summary>
    /// Retrieves an actor by its ID.
    /// </summary>
    Task<Actor?> GetByIdAsync(string id);
    
    /// <summary>
    /// Creates a new actor.
    /// </summary>
    Task<Actor> CreateAsync(Actor newActor);
    
    /// <summary>
    /// Updates an existing actor.
    /// </summary>
    Task UpdateAsync(string id, Actor updatedActor);
    
    /// <summary>
    /// Deletes an actor by its ID.
    /// </summary>
    Task DeleteAsync(string id);
}