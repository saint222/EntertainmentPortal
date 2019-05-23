using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EP.Sudoku.Web.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //GET api/values
        [HttpGet("api/players")]
        public async Task<IActionResult> GetAllPlayerAsync()
        {
            var result = await _mediator.Send(new GetAllPlayers());
            return result.Any() ? (IActionResult)Ok(result) : NotFound();
        }
        
    }
}
