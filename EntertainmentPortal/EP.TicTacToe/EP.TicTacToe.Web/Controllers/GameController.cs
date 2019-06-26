using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using EP.TicTacToe.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.TicTacToe.Web.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET api/game/id
        [HttpGet("api/game/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Game>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game is not found")]

        public async Task<IActionResult> GetGameAsync(uint id)
        {
            var result = await _mediator.Send(new GetGame { Id = id });
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        // POST api/game/id
        [HttpPost("api/game/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]

        public async Task<IActionResult> AddGameAsync()
        {
            var result = await _mediator.Send(new AddNewGameCommand());
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }
    }
}

