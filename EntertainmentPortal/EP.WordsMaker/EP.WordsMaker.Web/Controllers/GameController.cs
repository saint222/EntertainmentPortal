using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using EP.WordsMaker.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.WordsMaker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   //[Authorize]
    public class GameController : ControllerBase
    {
	    private readonly IMediator _mediator;

	    public GameController(IMediator mediator)
	    {
		    _mediator = mediator;
	    }

		[HttpGet]
	    [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Game>), Description = "Success")]
	    [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game collection is empty")]
	    public async Task<IActionResult> GetAllGamesAsync()
	    {
		    var result = await _mediator.Send(new GetAllGames());
		    return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
	    }

		[HttpGet("{PlayerId}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Game>), Description = "Success")]
		[SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game not found")]
		public async Task<IActionResult> GetGameAsync(string playerId)
        {
			var result = await _mediator.Send(new GetGameCommand(){PlayerId = playerId });
			return result.IsSuccess ? Ok(result.Value) : (IActionResult)NotFound(result.Error);
		}

		[HttpPost]
		[SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success. Game created")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]

		public async Task<IActionResult> AddNewGameAsync([FromBody] AddNewGameCommand model)
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
	}
}
