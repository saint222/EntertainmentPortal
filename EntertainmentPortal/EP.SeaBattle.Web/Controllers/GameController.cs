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

        [HttpPost("api/StartGame")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Start the game")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't start the game")]
        public async Task<IActionResult> StartGame([FromBody, NotNull, CustomizeValidator(RuleSet = "GamePreValidation")] StartGameCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }
        
        [HttpPut("api/FinishGame")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Finish the game")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't finish the game")]
        public async Task<IActionResult> FinishGame([FromBody, NotNull, CustomizeValidator(RuleSet = "GamePreValidation")] FinishGameCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
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
