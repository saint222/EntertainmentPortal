using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EP.DotsBoxes.Web.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GameBoardController : ControllerBase
    {
        private readonly IMediator _mediator;

         public GameBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/gameboard
        [HttpGet("api/gameboard")]
        public async Task<IActionResult> GetGameBoardAsync()
        {
            var result = await _mediator.Send<int[,]>(new GetGameBoard());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        //PUT: api/gameboard/10&10
        [HttpPut("api/gameboard/{row}&{column}")]
        public async Task<IActionResult> SetSizeAsync(string row, string column)
        {
            var result = await _mediator.Send <int[,]> (new SetSize(new string[] {  row, column }));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

    }
}