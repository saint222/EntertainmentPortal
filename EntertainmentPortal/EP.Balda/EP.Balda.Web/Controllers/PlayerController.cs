using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

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
        public async Task<IActionResult> GetPlayerAsync([FromQuery, 
            CustomizeValidator(RuleSet = "GetPlayerPreValidation")] GetPlayer model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);
            return result.HasValue ? (IActionResult) Ok(result.Value) : NotFound();
        }

        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description =
            "List of players is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers()).ConfigureAwait(false);
            return result.HasValue ? (IActionResult) Ok(result.Value) : NotFound();
        }

        [HttpPost("api/player/create")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Player can't be created")]
        public async Task<IActionResult> CreateNewPlayerAsync([FromBody,
            CustomizeValidator(RuleSet = "CreatePlayerPreValidation")] CreateNewPlayerCommand model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: Player: " +
                $"Login = {model.Login}, NickName = {model.NickName}, Password = {model.Password}");

            var (isSuccess, isFailure, value, error) = await _mediator.Send(model);

            if (isFailure)
                _logger.LogWarning(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Login = {model.Login}, NickName = {model.NickName}) - can't be written");

            return isSuccess
                ? (IActionResult) Created("api/players", value)
                : BadRequest(error);
        }

        [HttpPut("api/player/word")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Invalid data")]
        public async Task<IActionResult> AddWordAsync(
            [FromBody] AddWordToPlayerCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {model.Id}, GameId = {model.GameId} CellsFormWord = {model.CellsIdFormWord}");

            var (isSuccess, isFailure, value, error) = await _mediator.Send(model);
            if (isSuccess)
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"The word was written at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

            if (!isFailure) return Ok(value);
            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"Id = {model.Id}, CellsFormWord = {model.CellsIdFormWord}) - here the word can not be written");
            return BadRequest(error);

        }

        [HttpDelete("api/player/delete")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(Player), Description =
            "Player deleted")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Player), Description =
            "Player can't be deleted")]
        public async Task<IActionResult> DeletePlayerAsync(
            [FromBody,
            CustomizeValidator(RuleSet = "DeletePlayerPreValidation")] DeletePlayerCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {model.Id}");

            var (isSuccess, error) = await _mediator.Send(model);
            return isSuccess
                ? (IActionResult) NoContent()
                : BadRequest(error);

        }
    }
}