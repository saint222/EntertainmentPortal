using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("api/game/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description =
            "Game not found")]
        public async Task<IActionResult> GetGameAsync(long id)
        {
            var result = await _mediator.Send(new GetGame {Id = id});
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }

        [HttpPost("api/game")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Game can't be created")]
        public async Task<IActionResult> CreateNewGameAsync()
        {
            var result = await _mediator.Send(new CreateNewGameCommand());
            return result != null ? (IActionResult) Ok(result) : BadRequest();
        }
    }
}