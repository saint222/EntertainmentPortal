using System;
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
<<<<<<< HEAD
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
=======
        public async Task<IActionResult> GetGameAsync([FromQuery] GetGame model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {model.Id}");

            var result = await _mediator.Send(model);

            if (!result.HasNoValue) return Ok(result.Value);
            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} : Id = {model.Id}, - game not not found");
            return NotFound();
>>>>>>> dev_s
        }

        [HttpPost("api/game")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Game can't be created")]
<<<<<<< HEAD
        public async Task<IActionResult> CreateNewGameAsync([FromBody]CreateNewGameCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: PlayerId = {model.PlayerId}, MapSize = {model.MapSize}");

            var result = await _mediator.Send(model);

            if (result.IsFailure)
            {
                _logger.LogWarning($"Action: { ControllerContext.ActionDescriptor.ActionName} : - game can't be created");
                return BadRequest(result.Error);
            }
            return Created("api/game", result.Value);
=======
        public async Task<IActionResult> CreateNewGameAsync(
            [FromBody] CreateNewGameCommand model)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: PlayerId = {model.PlayerId}, MapSize = {model.MapSize}");

            var (isSuccess, isFailure, value, error) = await _mediator.Send(model);

            if (isSuccess)
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"game was created at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

            if (!isFailure) return Created("api/game", value);
            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                "game can't be created");
            return BadRequest(error);
>>>>>>> dev_s
        }
    }
}