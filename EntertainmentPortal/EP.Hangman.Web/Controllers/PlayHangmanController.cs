using System;
using System.Net;
using System.Threading.Tasks;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Web.Filters;
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
        [HttpGet("id")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ControllerData), Description = "Red")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(ControllerData), Description = "Session not found")]
        public async Task<IActionResult> GetUserSessionAsync(string id)
        {
            var result = await _mediator.Send(new GetUserSession(id));
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound(result.Error);
        }

        //POST: api/PlayHangman
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, typeof(ControllerData), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ControllerData), Description = "Object didn't create")]
        public async Task<IActionResult> CreateNewGameAsync()
        {
            var result = await _mediator.Send(new CreateNewGameCommand());
            return result.IsFailure ? BadRequest(result.Error) : (IActionResult) Created("Success", result.Value); 
        }

        //PUT: api/PlayHangman
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ControllerData), Description = "Updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ControllerData), Description = "Data didn't update")]
        [ValidationFilter]
        public async Task<IActionResult> CheckLetterAsync([FromBody]ControllerData model) 
        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
            var result = await _mediator.Send(new CheckLetterCommand(model));
            return result.IsSuccess ? (IActionResult)Ok(result) : BadRequest(result.Error);
        }

        //DELETE: api/PlayHangman
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(ControllerData), Description = "Deleted")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ControllerData), Description = "Data didn't delete")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteGameSessionAsync([FromBody]ControllerData model)
        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
            var result = await _mediator.Send(new DeleteGameSessionCommand(model));
            return result.IsSuccess ? (IActionResult) Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
