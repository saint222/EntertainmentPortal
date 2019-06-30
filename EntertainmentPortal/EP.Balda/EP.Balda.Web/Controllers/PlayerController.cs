using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(IMediator mediator, ILogger<PlayerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("api/player")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Player not found")]
        public async Task<IActionResult> GetPlayerAsync([FromQuery] GetPlayer model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);

            if (result.HasValue)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameter: Id = {model.Id}");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Id = {model.Id} - Player not found");

                return NotFound();
            }
        }

        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "List of players is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers());

            if (result.HasValue)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName}");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"- List of players is empty");

                return NotFound();
            }
        }

        [HttpPost("api/player/create")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Player can't be created")]
        public async Task<IActionResult> CreateNewPlayerAsync([FromBody] CreateNewPlayerCommand model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: Player: " +
                $"Login = {model.Login}, NickName = {model.NickName}, Password = {model.Password}");

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Login = {model.Login}, NickName = {model.NickName}" +
                $"Player was created at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return (IActionResult)Created("api/players", result.Value);
            }
            else
            {
                _logger.LogWarning(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Login = {model.Login}, NickName = {model.NickName}) - can't be written");

                return BadRequest(result.Error);
            }
        }

        [HttpPut("api/player/word")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Invalid data")]
        public async Task<IActionResult> AddWordAsync([FromBody] AddWordToPlayerCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {model.Id}, GameId = {model.GameId}");

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
                $"Id = {model.Id} - Word can't be written");

                return BadRequest(result.Error);
            }
        }

        [HttpDelete("api/player/delete")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description =
            "Player successfully deleted")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Player), Description =
            "Player can't be deleted")]
        public async Task<IActionResult> DeletePlayerAsync([FromBody] DeletePlayerCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {model.Id}");

            var result = await _mediator.Send(model);

            if(result.IsSuccess)
            {
                _logger.LogInformation(
                   $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                   $"Player with id {model.Id} was deleted at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"Id = {model.Id} - Player can't be deleted");

                return BadRequest(result.Error);
            }
        }
    }
}