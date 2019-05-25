using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EP.Hangman.Web;
using MediatR;
using EP.Hangman.Logic.Queries;

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

        //GET: api/PlayHangman
        [HttpGet]
        public async Task<IActionResult> GetHangmanAsync()
        {
            var result = await _mediator.Send(new GetHangman());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        //POST: api/PlayHangman
        [HttpPost]
        public async Task<IActionResult> PostHangmanAsync()
        {
            var result = await _mediator.Send(new PostHangman());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        //PUT: api/PlayHangman/{letter}
        [HttpPut("{letter}")]
        public async Task<IActionResult> CheckLetterAsync(string letter)
        {
            var result = await _mediator.Send(new PutHangman(letter));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
