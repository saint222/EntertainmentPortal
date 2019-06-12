using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MapController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/map/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Map not found")]
        public async Task<IActionResult> GetMapAsync([FromRoute]long id)
        {
            var result = await _mediator.Send(new GetMap(id)).ConfigureAwait(false);
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }
    }
}