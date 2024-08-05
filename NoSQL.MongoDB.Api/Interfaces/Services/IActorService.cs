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
    
    Task<Actor?> GetByIdAsync(string id);
    
    Task<Actor> CreateAsync(Actor newActor);
    
    Task UpdateAsync(string id, Actor updatedActor);
    
    Task DeleteAsync(string id);
}