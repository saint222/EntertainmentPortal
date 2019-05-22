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
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetDeck(1));
            return result!=null ? (IActionResult)Ok(result) : NotFound();
        }

        // POST: api/Deck
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Deck/5
        [HttpPut("{tile}")]
        public async Task<IActionResult> Put(int tile)
        {
            var result = await _mediator.Send(new MoveTile(1,tile));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
