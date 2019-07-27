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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EP.SeaBattle.Web.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> AddPlayerAsync([ CustomizeValidator(RuleSet = "AddPlayerPreValidation")]string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AddNewPlayerCommand model = new AddNewPlayerCommand(){ NickName = name, UserId = User.FindFirst("sub")?.Value };
            var result = await _mediator.Send(model);
            return result.IsFailure 
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }
        


        [HttpGet("api/GetPlayer")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Get a player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Player not exist")]
        public async Task<IActionResult> GetPlayer()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(new GetPlayerQuery() { UserId = User.FindFirst("sub")?.Value });
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
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
            model.UserId = User.FindFirst("sub")?.Value;
            var result = await _mediator.Send(model);
            return result.IsFailure ?
                (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

    }
}