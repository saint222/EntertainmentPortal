using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EP.SeaBattle.Web.Controllers
{

    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("api/StartGame")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Start the game")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't start the game")]
        public async Task<IActionResult> StartGame()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new StartGameCommand() { UserId = User.FindFirst("sub")?.Value });
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("api/FinishGame")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Finish the game")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't finish the game")]
        public async Task<IActionResult> FinishGame()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new FinishGameCommand() { UserId = User.FindFirst("sub")?.Value });
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpGet("api/GetAllGames")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Game>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game collection is empty")]
        public async Task<IActionResult> GetAllGamesAsync()
        {
            var result = await _mediator.Send(new GetAllGamesQuery());
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }
    }
}
