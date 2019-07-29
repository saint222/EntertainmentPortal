using System.Net;
using System.Threading.Tasks;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP._15Puzzle.Web.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IMediator _mediator;


        public DeckController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/Deck
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetDeckQuery(HttpContext.Request.Headers["Email"]));
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound("Start page");
        }

        // POST: api/Deck
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Post([FromBody] NewDeckCommand newDeckCommand)
        {
            newDeckCommand.Email = HttpContext.Request.Headers["Email"];
            var result = await _mediator.Send(newDeckCommand);
            if (result.IsSuccess)
            { 
                
                return (IActionResult)Ok(result.Value);
            }
            
            return NotFound(result.Error);
        }



        // PUT: api/Deck
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Put([FromBody][NotNull] int tile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _mediator.Send(new MoveTileCommand(HttpContext.Request.Headers["Email"], tile));
            
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);

        }

       
        
    }
    
}
