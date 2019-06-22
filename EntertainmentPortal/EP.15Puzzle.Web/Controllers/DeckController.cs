using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Logging;
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
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Get()
        {
            
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new GetDeckQuery(id));
                return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound(result.Error);
            }
            return NotFound("StartPage");
        }

        // POST: api/Deck
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Post()
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                var id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new ResetDeckCommand(id));
                return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);
            }
            else
            {
                var result = await _mediator.Send(new NewDeckCommand());
                if (result.Item1.IsSuccess)
                {
                    HttpContext.Response.Cookies.Append("id", result.Item2);
                    return (IActionResult) Ok(result.Item1.Value);
                }

                return NotFound(result.Item1.Error);


                //return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound(result.Error);
            }
        }

        

        // PUT: api/Deck
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Put([FromBody][NotNull] int tile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {

                var id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new MoveTileCommand(){Id = id, Tile = tile});
                return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);
            }
            return NotFound();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET: api/Deck/leaderboard
        [HttpGet("leaderboard")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Record), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> GetLeaderboard()
        {

            var result = await _mediator.Send(new GetLeaderboardCommand());
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound(result.Error);

        }
        
    }
    
}
