using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NoSQL.MongoDB.Api.Interfaces.Services;
using NoSQL.MongoDB.Api.Models;

namespace NoSQL.MongoDB.Api.Features.Actors;

public class ActorsController : BaseApiController
{
    private readonly IActorService _actorService;

    public ActorsController(IActorService actorService)
    {
        _actorService = actorService;
    }

    /// <summary>
    /// Get all actors
    /// </summary>
    /// <returns>The actor list</returns>
    [HttpGet]
    public async Task<ActionResult<List<Actor>>> Get()
    {
        return await _actorService.GetAsync();
    }

    /// <summary>
    /// Get an actor by id
    /// </summary>
    /// <param name="id">The actor id</param>
    /// <returns>The actor</returns>
    [HttpGet("{id}")]
    public async Task<Results<NotFound, Ok<Actor>>>  GetById(string id)
    {
        var actor = await _actorService.GetByIdAsync(id);
        return actor is null ? TypedResults.NotFound() : TypedResults.Ok(actor);
    }

    /// <summary>
    /// Create a new actor
    /// </summary>
    /// <param name="newActor">The new actor</param>
    /// <returns>The created actor</returns>
    [HttpPost]
    public async Task<Results<BadRequest, Created<Actor>>> Create(Actor newActor)
    {
        if (!string.IsNullOrEmpty(newActor.Id))
        {
            return TypedResults.BadRequest();
        }

        var actor = await _actorService.CreateAsync(newActor);

        var actionName = nameof(GetById); 
        var location = Url.Action(actionName, new { id = actor.Id });
        return TypedResults.Created(location, actor);
    }
    
    /// <summary>
    /// Delete an actor by id
    /// </summary>
    /// <param name="id">The actor id</param>
    /// <returns></returns>
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Get the actor by id
        var actor = await _actorService.GetByIdAsync(id);
        if (actor is null)
        {
            return NotFound();
        }
        
        await _actorService.DeleteAsync(id);
        return NoContent();
    }
    
    /// <summary>
    /// Update an actor
    /// </summary>
    /// <param name="id">The actor id to update</param>
    /// <param name="updatedActor">The updated values</param>
    /// <returns></returns>
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Actor updatedActor)
    {
        // Get the actor by id
        var actor = await _actorService.GetByIdAsync(id);
        if (actor is null)
        {
            return NotFound();
        }
        
        updatedActor.Id = id;
        
        await _actorService.UpdateAsync(id, updatedActor);
        return NoContent();
    }
}