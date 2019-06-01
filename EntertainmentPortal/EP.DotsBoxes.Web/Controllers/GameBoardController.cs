using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using NSwag.Annotations;

namespace EP.DotsBoxes.Web.Controllers
{
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
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<GameBoard>), Description = "Received new game board")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game board is not created")]
        public async Task<IActionResult> GetGameBoardAsync()
        {
            var result = await _mediator.Send(new GetGameBoard()).ConfigureAwait(false);
            return result.Any() ? (IActionResult)Ok(result) : NotFound();
        }

        // POST api/gameboard
        [HttpPost("api/gameboard")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(GameBoard), Description = "Create new game board")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> PostNewGameBoardAsync([FromBody]GameBoard model)
        {
           var result = await _mediator.Send(new CreateNewGameBoardCommand
            {
                Rows = model.Rows,
                Columns = model.Columns
            });

            return Ok(result);
        }
    }
}