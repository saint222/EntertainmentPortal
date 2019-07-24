﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NJsonSchema.Annotations;
using NSwag.Annotations;

namespace EP.Sudoku.Web.Controllers
{
    /// <summary>
    /// Here are CRUD operations that touch upon players of the game.
    /// </summary>
    [ApiController]    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlayersController> _logger;

        /// <summary>
        /// Is used for DI usage.
        /// </summary>
        public PlayersController(IMediator mediator, ILogger<PlayersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Fetches all registered players from the Db.
        /// </summary>  
        [HttpGet("api/players")]        
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetAllPlayerAsync()
        {
            var players = await _mediator.Send(new GetAllPlayers());

            return players.Any() ? (IActionResult)Ok(players) : NotFound();
        }

        /// <summary>
        /// Fetches a player of the game from the Db by the unique UserId.
        /// </summary>
        [HttpGet("api/player")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetPlayerByUserIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = User.FindFirst("sub")?.Value;
                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogError($"Incorrect value for the player's Id was set. '{id}' - is == null...");
                    return BadRequest();
                }
            }
            var player = await _mediator.Send(new GetPlayerByUserId(id));

            return player.HasValue ? (IActionResult)Ok(player.Value) : NotFound();
        }

        /// <summary>
        /// Fetches a player of the game from the Db by the unique Id.
        /// </summary>
        [HttpGet("api/players/{id}")]        
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetPlayerByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Incorrect value for the player's Id was set. '{id}' - is <= 0...");
                return BadRequest();
            }
            var player = await _mediator.Send(new GetPlayerById(id));

            return player != null ? (IActionResult)Ok(player) : NotFound();
        }

        /// <summary>
        /// Creates a new player and saves information in the Db.
        /// </summary>
        [HttpPost("api/players")]       
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> CreatePlayer([FromBody, NotNull, CustomizeValidator(RuleSet = "PreValidationPlayer")] CreatePlayerCommand model)
        {                   
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model for createint a new player was used.");
                return BadRequest();                
            }

            model.UserId = User.FindFirst("sub")?.Value; //takes UserID from requests
            var result = await _mediator.Send(model);

            return result.IsFailure ? (IActionResult)BadRequest(result.Error) : Ok(result.Value); //FP is here           
        }

        /// <summary>
        /// Changes the known information about a chosen player and saves it in the Db.
        /// </summary>
        [HttpPut("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> EditPlayer([FromBody, NotNull, CustomizeValidator(RuleSet = "PreValidationEditPlayer")]UpdatePlayerCommand model)
        {
            if (!ModelState.IsValid)
            {                
                return BadRequest();
            }
            var result = await _mediator.Send(model);

            return result.IsFailure ? (IActionResult)BadRequest(result.Error) : Ok(result.Value); //FP is here
        }

        /// <summary>
        /// Removes the whole information about a chosen player from the Db.
        /// </summary>
        [HttpDelete("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (id <= 0)
            {
                _logger.LogError($"Incorrect value for the player's Id was set. '{id}' - is <= 0...");
                return BadRequest();
            }
            var result = await _mediator.Send(new DeletePlayerCommand(id)); 
            
            return result == true ? (IActionResult)Ok() : BadRequest();
        }
    }
}
