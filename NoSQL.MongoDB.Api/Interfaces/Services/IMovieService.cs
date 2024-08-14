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
    
    /// <summary>
    /// Creates a new movie.
    /// </summary>
    Task<Movie> CreateAsync(Movie newMovie);
    
    /// <summary>
    /// Updates an existing movie.
    /// </summary>
    Task UpdateAsync(string id, Movie updatedMovie);
    
    /// <summary>
    /// Deletes a movie by its ID.
    /// </summary>
    Task DeleteAsync(string id);
}