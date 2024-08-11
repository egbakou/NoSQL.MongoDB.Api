using System.Collections.Generic;
using System.Threading.Tasks;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Interfaces.Services;

/// <summary>
/// Interface for movie service operations.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Retrieves a list of movies.
    /// </summary>
    Task<List<Movie>> GetAsync();
    
    /// <summary>
    /// Retrieves a movie by its ID.
    /// </summary>
    Task<Movie?> GetByIdAsync(string id);
    
    Task<Movie> CreateAsync(Movie newMovie);
    
    Task UpdateAsync(string id, Movie updatedMovie);
    
    Task DeleteAsync(string id);
}