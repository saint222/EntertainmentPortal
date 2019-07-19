using EP.Balda.Data.Models;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class PlayerController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlayerController> _logger;
        private readonly UserManager<PlayerDb> _manager;

        public PlayerController(IMediator mediator, ILogger<PlayerController> logger, UserManager<PlayerDb> manager)
        {
            _mediator = mediator;
            _logger = logger;
            _manager = manager;
        }

        [HttpGet("api/player")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
        public async Task<IActionResult> GetPlayerAsync([FromQuery]string id)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {id}");

            var user = await _manager.FindByIdAsync(id);
            
            if (user != null)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameter: Id = {id}");

                return Ok(user);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Id = {id} - Player not found");

                return NotFound();
            }
        }

        [HttpGet("api/players")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "List of players is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var users = await _manager.Users.ToArrayAsync();
            
            if (users.Any())
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName}");

                return Ok(users);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"- List of players is empty");

                return NotFound();
            }
        }

        [HttpDelete("api/player/delete")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Player successfully deleted")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Player), Description = "Player can't be deleted")]
        public async Task<IActionResult> DeletePlayerAsync([FromQuery]string id)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {id}");

            var user = await _manager.FindByIdAsync(id);

            if (user != null)
            {
                var status = await _manager.DeleteAsync(user);

                if (status.Succeeded)
                {
                    _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} : - Player with id {id} was deleted at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                    return Ok(user);
                }
                
                return status.Succeeded ? (IActionResult)Ok() : BadRequest(status.Errors);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: Id = {id} - Player can't be deleted");

                return BadRequest("Player can't be deleted");
            }
        }

        [HttpGet("api/player/words")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Player successfully deleted")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(Player), Description = "Player can't be deleted")]
        public async Task<IActionResult> GetPlayersWordsAsync([FromQuery]long gameId)
        {
            _logger.LogDebug(
                           $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: gameId = {gameId}");

            var result = await _mediator.Send(new GetPlayersWords() { GameId = gameId, PlayerId = UserId });

            if (result.Count() != 0)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameters: gameId = {gameId}");

                return Ok(result);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Parameters: gameId = {gameId}");

                return BadRequest();
            }
        }
    }
}