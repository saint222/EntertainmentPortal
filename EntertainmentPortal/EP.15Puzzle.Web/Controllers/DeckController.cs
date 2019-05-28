using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace EP._15Puzzle.Web.Controllers
{
    /// <summary>
    /// now userID not used in GET and PUT methods, used id=1 injected into controller's methods. will be changed after understood how to hide in body
    /// </summary>
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetDeck(id));
            return result!=null ? (IActionResult)Ok(result) : NotFound();
        }

        // POST: api/Deck/id
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            var result = await _mediator.Send(new NewDeck(id));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        // PUT: api/Deck/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] int tile)
        {
            var result = await _mediator.Send(new MoveTile(id,15));
            return result != null ? (IActionResult)Ok(result) : NotFound();
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
