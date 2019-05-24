using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EP.WordsMaker.Logic.Queries;

namespace EP.WordsMaker.Web.Controllers
{
	[ApiController]
	public class PlayerController : ControllerBase
	{
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }
  
		[HttpGet("api/players")]
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers());
            return result.Any() ? (IActionResult)Ok(result) : NotFound();
        }


    }
}
