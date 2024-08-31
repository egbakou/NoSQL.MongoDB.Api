using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoSQL.MongoDB.Api.Interfaces.Services;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Features.Movies;

/// <summary>
/// Movies REST API Controller
/// </summary>
/// <param name="movieService">Movie service</param>
public class MoviesController(IMovieService movieService) : BaseApiController
{
    /// <summary>
    /// Get all movies
    /// </summary>
    /// <returns>The movie list</returns>
    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAsync()
    {
        var movies = await movieService.GetAsync();
        return movies;
    }
    
    /// <summary>
    /// Get a movie by id
    /// </summary>
    /// <param name="id">The movie id</param>
    /// <returns>The movie</returns>
    [HttpGet("{id:length(24)}")]
    public async Task<Results<NotFound, Ok<Movie>>> GetByIdAsync(string id)
    {
        var movie = await movieService.GetByIdAsync(id);
        return movie is null ? TypedResults.NotFound(): TypedResults.Ok(movie);
    }
    
    /// <summary>
    /// Create a new movie
    /// </summary>
    /// <param name="newMovie">The new movie</param>
    /// <returns>The created movie</returns>
    [HttpPost]
    // TypedResults metadata are not inferred for API Controllers: https://github.com/dotnet/aspnetcore/issues/44988
    public async Task<Results<BadRequest, Created<Movie>>> CreateAsync(Movie newMovie)
    {
        if (!string.IsNullOrEmpty(newMovie.Id))
        {
            return TypedResults.BadRequest();
        }
        
        var movie = await movieService.CreateAsync(newMovie);
        
        var actionName = nameof(GetByIdAsync); // https://stackoverflow.com/questions/71117926/cannot-resolve-action-in-createdatactionnameofactionname
        var location = Url.Action(actionName, new { id = movie.Id });
        return TypedResults.Created(location, movie);
    }
    
    
    /// <summary>
    /// Update a movie
    /// </summary>
    /// <param name="id">The movie id to update</param>
    /// <param name="updatedMovie">The updated values</param>
    /// <returns></returns>
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateAsync(string id, Movie updatedMovie)
    {
        await movieService.UpdateAsync(id, updatedMovie);
        return NoContent();
    }
}