using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using NoSQL.MongoDB.Api.Database;
using NoSQL.MongoDB.Api.Interfaces.Services;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Features.Movies;

/// <summary>
/// Service for managing movie operations.
/// </summary>
public class MovieService(DbContext dbContext) : IMovieService
{
    private readonly IMongoCollection<Movie> _movies = dbContext.Movies;

    /// <inheritdoc/>
    public async Task<List<Movie>> GetAsync()
    {
        // take only the first 10 movies
        return await _movies.Find(_ => true)
            .Limit(10)
            .ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(string id)
    {
        return await _movies.Find(movie => movie.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Movie> CreateAsync(Movie newMovie)
    {
        await _movies.InsertOneAsync(newMovie);
        return newMovie;
    }

    // On update : MongoDB.Driver.MongoWriteException: A write operation resulted in an error. WriteError: { Category : "Uncategorized", Code : 66, Message : "After applying the update, the (immutable) field '_id' was found to have been altered to _id: null" }.
    // Avoid updating the Id field or setting it to null, updatedMovie.Id should be the same as id when updating
    public async Task UpdateAsync(string id, Movie updatedMovie)
    {
        await _movies.ReplaceOneAsync(movie => movie.Id == id, updatedMovie);
    }

    public async Task DeleteAsync(string id)
    {
        await _movies.DeleteOneAsync(movie => movie.Id == id);
    }
}