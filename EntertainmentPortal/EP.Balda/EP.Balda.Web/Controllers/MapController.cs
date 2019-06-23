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

        [HttpGet("api/map")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Map not found")]
<<<<<<< HEAD
        public async Task<IActionResult> GetMapAsync([FromQuery]GetMap model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
=======
        public async Task<IActionResult> GetMapAsync([FromQuery] GetMap model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);
            return result.HasValue ? (IActionResult) Ok(result.Value) : NotFound();
>>>>>>> dev_s
        }
    }
}