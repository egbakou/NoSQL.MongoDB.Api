using Microsoft.AspNetCore.Mvc;

namespace NoSQL.MongoDB.Api.Features;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class BaseApiController : ControllerBase
{
    
}