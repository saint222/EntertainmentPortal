using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
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
        public async Task<IActionResult> GetPlayerAsync([FromQuery]string userName)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: userName = {userName}");

            var user = await _manager.FindByNameAsync(userName);
            
            if (user != null)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameter: userName = {userName}");

                return Ok(user);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"userName = {userName} - Player not found");

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
        
        [HttpPut("api/player/word")] //move to game controller
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddWordAsync([FromBody] AddWordToPlayerCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: Id = {model.Id}, GameId = {model.GameId}");

            model.Id = UserId;
            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"The word was written at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"Id = {model.Id} - Word can't be written");

                return BadRequest(result.Error);
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
    }
}