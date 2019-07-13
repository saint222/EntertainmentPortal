using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using EP.SeaBattle.Logic.Queries;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace EP.SeaBattle.Web.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/AddPlayer")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Add new player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddPlayerAsync([FromBody, NotNull, CustomizeValidator(RuleSet = "AddPlayerPreValidation")]AddNewPlayerCommand model)
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


        [HttpGet("api/GetAllPlayers")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Players collection is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayersQuery());
            return result.HasValue 
                ? (IActionResult)Ok(result.Value) 
                : NotFound();
        }

        [HttpGet("api/GetPlayer")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Get a player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Player not exist")]
        public async Task<IActionResult> GetPlayer([NotNull] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(new GetPlayerQuery() { Id = id });
            return result.IsSuccess ? Ok(result.Value)
                : (IActionResult)NotFound(result.Error);
        }


        [HttpPut("api/UpdatePlayer")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Update player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> UpdatePlayerAsync([FromBody, NotNull, CustomizeValidator(RuleSet = "UpdatePlayerPreValidation")]UpdatePlayerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsFailure ?
                (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }
    }
}