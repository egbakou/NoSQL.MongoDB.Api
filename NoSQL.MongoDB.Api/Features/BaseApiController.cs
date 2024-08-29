using Microsoft.AspNetCore.Mvc;

namespace NoSQL.MongoDB.Api.Features;

/// <summary>
/// Base Api controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class BaseApiController : ControllerBase
{
    
}