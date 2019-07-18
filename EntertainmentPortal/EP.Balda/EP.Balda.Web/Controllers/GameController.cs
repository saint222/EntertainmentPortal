using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using EP.Balda.Web.Models;
using EP.Balda.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class GameController : BaseController
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
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game not found")]
        public async Task<IActionResult> GetGameAsync([FromQuery] long id)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {id}");

            var result = await _mediator.Send(new GetGame() { Id = id });

            if (result.HasValue)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameter: Id = {id}");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName} : " +
                    $"Id = {id}, - game not found");
                return NotFound();
            }
        }

        [HttpPost("api/game")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Game can't be created")]
        public async Task<IActionResult> CreateNewGameAsync([FromBody] int mapSize)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if(!isAuthenticated)
            {
                return BadRequest("User is not authorized");
            }

            var model = new CreateNewGameCommand();
            model.PlayerId = UserId;
            model.MapSize = mapSize;
            
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: PlayerId = {model.PlayerId}, MapSize = {model.MapSize}");

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"Game was created at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                //return Created("api/game", result.Value);
                return Created("api/game", result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                "Game can't be created");

                return BadRequest(result.Error);
            }
        }

        [HttpPut("api/game/word")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddWordAsync([FromBody] GameAndCells gameAndCells)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: gameId = {gameAndCells.GameId}");

            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return BadRequest("User is not authorized");
            }

            var model = new AddWordToPlayerCommand();
            model.PlayerId = UserId;
            model.GameId =  gameAndCells.GameId;
            model.CellsIdFormWord = gameAndCells.CellsIdFormWord;

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"The word was written at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"Id = {model.PlayerId} - Word can't be written");

                return BadRequest(result.Error);
            }
        }
    }
}