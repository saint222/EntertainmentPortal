using System.Collections.Generic;
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
using Microsoft.AspNetCore.Authorization;

namespace EP.DotsBoxes.Web.Controllers
{
    /// <summary>
    /// This is PlayersController.
    /// </summary>
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlayersController> _logger;

        /// <summary>
        /// PlayersController constructor. Is used for DI.
        /// </summary>
        public PlayersController(IMediator mediator, ILogger<PlayersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Select a player of the game from the Db by the unique Id.
        /// </summary>
        // GET api/player/{id}
        [HttpGet("api/player/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        public async Task<IActionResult> GetPlayerAsync([FromRoute]int id)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName}, Parameters: id = {id}");
            var result = await _mediator.Send(new GetPlayer(id));
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

        /// <summary>
        /// Select all registered players from the Db.
        /// </summary>
        // GET api/players
        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Received collection of players")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Players collection is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName}");
            var result = await _mediator.Send(new GetAllPlayers());
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

        /// <summary>
        /// Creates a new player and saves data in the Db.
        /// </summary>
        // POST api/player
        [HttpPost("api/player")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Added new player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddPlayerAsync([FromBody][NotNull]AddPlayerCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: Player: " +
                $"Name = {model.Name}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                                   $"Name = {model.Name} - Invalid data");

                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
