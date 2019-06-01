using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EP.DotsBoxes.Logic.Queries;
using NSwag.Annotations;

namespace EP.DotsBoxes.Web.Controllers
{
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/players
        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Received collection of players")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Players collection is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers()).ConfigureAwait(false);
            return result.Any() ? (IActionResult)Ok(result) : NotFound();

        }

        // POST api/players
        [HttpPost("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Added new player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddPlayerAsync([FromBody]Player model)
        {
            var result = await _mediator.Send(new AddNewPlayerCommand
            {
                Name = model.Name,
                Color = model.Color,
                Score = model.Score
            });

            return Ok(result);
        }
    }
}
