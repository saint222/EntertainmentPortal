﻿using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/player/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        public async Task<IActionResult> GetPlayerAsync(long id)
        {
            var result = await _mediator.Send(new GetPlayer(id));
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "List of players is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers()).ConfigureAwait(false);
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

        [HttpPost("api/player/create")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Player can't be created")]
        public async Task<IActionResult> CreateNewPlayerAsync(AddNewPlayerCommand model)
        {
            var result = await _mediator.Send(model);
            return result.IsFailure ? (IActionResult)Ok(result.Value) : BadRequest();
        }
    }
}
