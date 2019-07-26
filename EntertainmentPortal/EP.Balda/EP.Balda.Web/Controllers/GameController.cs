using System;
using System.Net;
using System.Security.Claims;
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

        [HttpGet("api/currentGame")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(CurrentGame), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        public async Task<IActionResult> GetCurrentGame()
        {
            string UserId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;

        _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}");

            if (UserId != null)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName}");

                var result = await _mediator.Send(new GetCurrentGame() { Id = UserId });

                var cells = Helpers.Do2DimArray(result.Value.Map);
                var currentGame = new CurrentGame()
                {
                    Cells = cells,
                    IsGameOver = result.Value.IsGameOver,
                    GameId = result.Value.Id,
                    UserId = UserId
                };

                return Ok(currentGame);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}");

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

                return Created("api/game", result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                "Game can't be created");

                return BadRequest(result.Error);
            }
        }

        [HttpDelete("api/leaveGame")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(string), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Game can't be stopped")]
        public async Task<IActionResult> LeaveGameAsync([FromQuery]long gameID)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return BadRequest("User is not authorized");
            }

            var model = new LeaveGameCommand();
            model.GameId = gameID;

            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}");

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"Game was created at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Ok(result);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                "Game can't be created");

                return BadRequest(result.Error);
            }
        }

        [HttpPut("api/game/word")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
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
            model.GameId = gameAndCells.GameId;
            model.CellsThatFormWord = gameAndCells.CellsThatFormWord;

            var result = await _mediator.Send(model);

            string word = Helpers.FormWordFromCells(gameAndCells.CellsThatFormWord);


            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"The word {word} was written at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"Id = {model.PlayerId} - Word can't be written");

                return BadRequest("Word " + word + ": " + result.Error);
            }
        }
    }
}