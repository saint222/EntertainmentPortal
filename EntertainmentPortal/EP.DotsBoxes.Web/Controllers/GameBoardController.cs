using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.DotsBoxes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameBoardController : ControllerBase
    {
        private readonly IMediator _mediator;

         public GameBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/gameboard
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(int[,]), Description = "Array received")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Array is null")]
        public async Task<IActionResult> GetGameBoardAsync()
        {
            var result = await _mediator.Send(new GetGameBoard());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        // POST api/gameboard
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(int[,]), Description = "Array created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> PostNewGameBoardAsync([FromBody]GameBoard gameBoard)
        {
            var result = await _mediator.Send(new PostNewGameBoard(gameBoard));
            return Ok(result);
        }     
    }
}