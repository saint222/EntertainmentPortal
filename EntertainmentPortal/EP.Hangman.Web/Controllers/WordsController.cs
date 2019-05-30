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
    public class WordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //PUT: api/Words/"word"
        [HttpPut("{word}")]
        public async Task<IActionResult> SetWordAsync(string word)
        {
            var result = await _mediator.Send(new SetWord(word));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }
    }
}
