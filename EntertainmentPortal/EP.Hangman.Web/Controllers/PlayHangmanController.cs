using System.Net;
using System.Threading.Tasks;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EP.Hangman.Logic.Queries;
using NSwag.Annotations;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayHangmanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayHangmanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/PlayHangman/{id}
        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserGameData), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Session no found")]
        public async Task<IActionResult> GetUserSessionAsync(string id)
        {
            var result = await _mediator.Send(new GetUserSession(id));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        //POST: api/PlayHangman
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, typeof(UserGameData), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Object didn't create")]
        public async Task<IActionResult> CreateNewGameAsync()
        {
            var result = await _mediator.Send(new CreateNewGameCommand());
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }

        //PUT: api/PlayHangman/{id}
        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserGameData), Description = "Cool")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Data didn't update")]
        public async Task<IActionResult> CheckLetterAsync(string id, [FromBody]string letter) 
        {
            var result = await _mediator.Send(new CheckLetterCommand(id, letter));
            return result != null ? (IActionResult)Ok(result) : BadRequest();
        }
    }
}
