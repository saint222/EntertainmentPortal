using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Queries;
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

        // POST: api/Deck/id
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Post()
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new ResetDeck(id));
                return result != null ? (IActionResult)Ok(result) : NotFound();
            }
            else
            {
                var result = await _mediator.Send(new NewDeck());
                HttpContext.Response.Cookies.Append("id",result.UserId.ToString());
                return result != null ? (IActionResult)Ok(result) : NotFound();
            }
            //var result = await _mediator.Send(new NewDeck(id));
            //return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        // PUT: api/Deck/id
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Put([FromBody] int tile)
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {
                int id = int.Parse(HttpContext.Request.Cookies["id"]);
                var result = await _mediator.Send(new MoveTile(id, tile));
                return result != null ? (IActionResult)Ok(result) : NotFound();
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
