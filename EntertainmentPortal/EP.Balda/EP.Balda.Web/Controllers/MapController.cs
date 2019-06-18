using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MapController> _logger;

        public MapController(IMediator mediator, ILogger<MapController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("api/map/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Map not found")]
        public async Task<IActionResult> GetMapAsync([FromRoute]long id)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {id}");

            var result = await _mediator.Send(new GetMap(id)).ConfigureAwait(false);
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }
    }
}