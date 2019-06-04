using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bogus.Extensions;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.Sudoku.Web.Controllers
{
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/players")] 
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetAllPlayerAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers());
            return result.Any() ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpGet("api/players/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetPlayerByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(new GetPlayerById(id));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> CreatePlayer([FromBody]Player model)
        {
            if (model == null)
            {
                return BadRequest();
            }            
            var player = await _mediator.Send(new CreatePlayerCommand(model));
            return true ? (IActionResult)Ok() : NotFound();
        }

        [HttpPut("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> EditPlayer([FromBody]Player model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var player = await _mediator.Send(new UpdatePlayerCommand(model));
            return true ? (IActionResult)Ok() : NotFound();
        }

        [HttpDelete("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(new DeletePlayerCommand(id));
            
            return true ? (IActionResult)Ok() : NotFound();
        }
    }
}
