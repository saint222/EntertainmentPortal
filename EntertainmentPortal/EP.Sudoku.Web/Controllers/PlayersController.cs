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
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Sudoku.Web.Controllers
{
    [ApiController]
    public class PlayersController : ControllerBase
    {        
        private readonly IMediator _mediator;       
        private readonly ILogger<PlayersController> _logger;        

        public PlayersController(IMediator mediator, ILogger<PlayersController> logger)
        {
            _mediator = mediator;
            _logger = logger;           
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
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetPlayerByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"Incorrect value for the player's Id was set. '{id}' - is <= 0...");
                return BadRequest();
            }
            var player = await _mediator.Send(new GetPlayerById(id));
            return player != null ? (IActionResult)Ok(player) : NotFound();
        }

        [HttpPost("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> CreatePlayer([FromBody]Player model)
        {
            if (model == null)
            {                
                return BadRequest();
            }            
            var player = await _mediator.Send(new CreatePlayerCommand(model));
            return player!=null ? (IActionResult)Ok(player) : BadRequest();
        }

        [HttpPut("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> EditPlayer([FromBody]Player model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var player = await _mediator.Send(new UpdatePlayerCommand(model));
            return player != null ? (IActionResult)Ok(player) : BadRequest();
        }

        [HttpDelete("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"Incorrect value for the player's Id was set. '{id}' - is <= 0...");
                return BadRequest();
            }
            var result = await _mediator.Send(new DeletePlayerCommand(id));
            
            return true ? (IActionResult)Ok() : BadRequest();
        }
    }
}
