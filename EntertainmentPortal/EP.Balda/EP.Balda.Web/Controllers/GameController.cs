using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GameController> _logger;

        public GameController(IMediator mediator, ILogger<GameController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("api/game")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Game not found")]
        public async Task<IActionResult> GetGameAsync([FromQuery]GetGame model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);

            if (result.HasNoValue)
            {
                _logger.LogWarning($"Action: { ControllerContext.ActionDescriptor.ActionName} : Id = {model.Id}, - game not not found");
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPost("api/game")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Game can't be created")]
        public async Task<IActionResult> CreateNewGameAsync([FromBody]CreateNewGameCommand model)
        {
            var result = await _mediator.Send(model);

            if (result.IsFailure)
            {
                _logger.LogWarning($"Action: { ControllerContext.ActionDescriptor.ActionName} : - game can't be created");
                return BadRequest(result.Error);
            }
            return Created("", result.Value);
        }
    }
}