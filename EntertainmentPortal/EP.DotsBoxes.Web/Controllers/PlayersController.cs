using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EP.DotsBoxes.Logic.Queries;
using NSwag.Annotations;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace EP.DotsBoxes.Web.Controllers
{
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(IMediator mediator, ILogger<PlayersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET api/players
        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Received collection of players")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Players collection is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName}");
            var result = await _mediator.Send(new GetAllPlayers()).ConfigureAwait(false);
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

        // POST api/player
        [HttpPost("api/player")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Added new player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddPlayerAsync([FromBody][NotNull]AddPlayerCommand model) 
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: Player: " +
                $"Name = {model.Name}, Color = {model.Color}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
