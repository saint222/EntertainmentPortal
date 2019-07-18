using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using EP.Balda.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class MapController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MapController> _logger;

        public MapController(IMediator mediator, ILogger<MapController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("api/map")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Map not found")]
        public async Task<IActionResult> GetMapAsync([FromQuery] long id)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {id}");

            var result = await _mediator.Send(new GetMap() { Id = id });

            if (result.HasValue)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameter: Id = {id}");

                //return Ok(result.Value);

                var cells = Helpers.Do2DimArray(result.Value);
                return Ok(cells);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Id = {id} - Map not found");

                return NotFound();
            }
        }

        [HttpGet("api/map/alphabet")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Error")]
        public IActionResult GetAlphabet()
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}");

            var alphabet = Helpers.GetEnglishAlphabet();

            return alphabet != null ? (IActionResult)Ok(alphabet) : BadRequest();

        }
    }
}