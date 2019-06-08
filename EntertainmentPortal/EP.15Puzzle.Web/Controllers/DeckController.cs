using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new GetDeck(id));
                return result != null ? (IActionResult)Ok(result) : NotFound();
            }
            return NotFound();
        }

        // POST: api/Deck
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Post()
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new ResetDeckCommand(id));
                return result != null ? (IActionResult)Ok(result) : NotFound();
            }
            else
            {
                var result = await _mediator.Send(new NewDeckCommand());
                HttpContext.Response.Cookies.Append("id",result.UserId.ToString());
                return result != null ? (IActionResult)Ok(result) : NotFound();
            }
            //var result = await _mediator.Send(new NewDeck(id));
            //return result != null ? (IActionResult)Ok(result) : NotFound();
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
                return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound(result.Error);
            }
            return NotFound();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Tet
    {
        public int Tile { get; set; }
    }
}
