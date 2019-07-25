using System;
using System.Net;
using System.Threading.Tasks;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EP.TicTacToe.Logic.Queries;
using EP.TicTacToe.Web.Filters;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.TicTacToe.Web.Controllers
{
    [Route("api/step")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = "Google")]
    //[Authorize(AuthenticationSchemes = "Facebook")]
    public class StepController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StepController> _logger;

        public StepController(IMediator mediator, ILogger<StepController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPut("move")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(StepResult), Description = "Success")]
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
                    $"was written at [{DateTime.UtcNow}]");

                return Ok(result.Value);
            }

            _logger.LogWarning(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"step from PlayerId={model.PlayerId} GameId={model.GameId} can't be written");

            return BadRequest(result.Error);
        }
    }
}