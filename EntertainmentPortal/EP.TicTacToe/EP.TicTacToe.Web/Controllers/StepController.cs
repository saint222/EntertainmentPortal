using System;
using System.Net;
using System.Threading.Tasks;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EP.TicTacToe.Logic.Queries;
using EP.TicTacToe.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.TicTacToe.Web.Controllers
{
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StepController> _logger;

        public StepController(IMediator mediator, ILogger<StepController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("api/cell")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Cell), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Cell not found")]
        public async Task<IActionResult> GetCellAsync([FromQuery] GetCell model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {model.Id}");

            var result = await _mediator.Send(model);

            if (result.HasValue)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameter: Id = {model.Id}");

                return Ok(result.Value);
            }

            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}: Id = {model.Id} - Cell not found");

            return NotFound();
        }

        [HttpPut("api/cell")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Cell), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Invalid data")]
        public async Task<IActionResult> AddNewStepAsync(
            [FromBody] AddNewStepCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: GameId = {model.GameId}, PlayerId = {model.PlayerId}," +
                             $" index={model.Index}");

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : " +
                    $"step from PlayerId={model.PlayerId} GameId={model.GameId} " +
                    $"was written at [{DateTime.UtcNow.Kind}]");

                return Ok(result.Value);
            }

            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"step from PlayerId={model.PlayerId} GameId={model.GameId} can't be written");

            return BadRequest(result.Error);
        }
    }
}