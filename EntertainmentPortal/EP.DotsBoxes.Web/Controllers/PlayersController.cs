using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EP.DotsBoxes.Logic.Queries;

namespace EP.DotsBoxes.Web.Controllers
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
        public async Task<IActionResult> GetAllPlayersAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers());
            return result.Any() ? (IActionResult)Ok(result) : NotFound();

        }
    }
}
