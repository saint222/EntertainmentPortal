using System;
using System.Net;
using System.Threading.Tasks;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using EP.TicTacToe.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EP.TicTacToe.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.TicTacToe.Web.Controllers
{
    [Route("api/game")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = "Google")]
    //[Authorize(AuthenticationSchemes = "Facebook")]
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
        [ValidationFilter]
        public async Task<IActionResult> GetGameAsync([FromQuery] GetGame model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);

            if (result.HasValue)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                    $"Parameter: Id = {model.Id}");

                return Ok(result.Value);
            }

            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} : " +
                $"Id = {model.Id}, - game not found");
            return NotFound();
        }

        [HttpPost("api/game")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Game can't be created")]
        public async Task<IActionResult> CreateNewGameAsync(
            [FromBody] AddNewGameCommand model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameters: PlayerOne = {model.PlayerOne}, " +
                $"PlayerTwo = {model.PlayerTwo}, " +
                $"MapSize = {model.MapSize}");

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"Game was created at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Created("api/game", result.Value);
            }

            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                "Game can't be created");

            return BadRequest(result.Error);
        }
    }
}