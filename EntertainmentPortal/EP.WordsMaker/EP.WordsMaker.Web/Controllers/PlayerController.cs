using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EP.WordsMaker.Logic.Queries;
using Microsoft.AspNetCore.Authorization;
using NSwag.Annotations;

namespace EP.WordsMaker.Web.Controllers
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
  
		[HttpGet]
        [Authorize(AuthenticationSchemes = "Facebook")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player collection is empty")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers());
            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

		[HttpGet("{id}")]

		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Player>), Description = "Success")]
		[SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Player not found")]
		public async Task<IActionResult> GetPlayerAsync(int id)
		{
			var result = await _mediator.Send(new GetPlayerCommand(){Id = id});
			return result.IsSuccess ? Ok(result.Value): (IActionResult)NotFound(result.Error);

		}

		[HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddNewPlayerASync([FromBody] AddNewPlayerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsFailure ?
                (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        public async Task<IActionResult> DeletePlayerAsync([FromBody] DeletePlayerCommand model)
        {
            var result = await _mediator.Send(model);
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
