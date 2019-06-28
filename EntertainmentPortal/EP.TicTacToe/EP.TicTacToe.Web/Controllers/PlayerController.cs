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
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description =
            "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Player list is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers()).ConfigureAwait(false);
            return result.HasValue ? (IActionResult) Ok(result.Value) : NotFound();
        }

        // POST api/values
        [HttpPost("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Invalid data")]
        public async Task<IActionResult> AddPlayerAsync(
            [FromBody] [NotNull] AddNewPlayerCommand model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(model);
            return result.IsFailure
                ? (IActionResult) BadRequest(result.Error)
                : Ok(result.Value);
        }
    }
}