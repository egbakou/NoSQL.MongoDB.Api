using System.Collections.Generic;
using System.Threading.Tasks;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Interfaces.Services;

/// <summary>
/// Interface for movie service operations.
/// </summary>
public interface IMovieService
{
    Task<List<Movie>> GetAsync();
    
    Task<Movie?> GetByIdAsync(string id);
    
    Task<Movie> CreateAsync(Movie newMovie);
    
    Task UpdateAsync(string id, Movie updatedMovie);
    
    Task DeleteAsync(string id);
}