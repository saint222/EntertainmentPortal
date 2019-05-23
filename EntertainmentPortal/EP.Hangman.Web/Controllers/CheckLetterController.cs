using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Hangman.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EP.Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckLetterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheckLetterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //GET: api/CheckLetter/{letter}
        [HttpPut("{letter}")]
        public async Task<IActionResult> CheckLetterAsync(string letter)
        {
            var result = await _mediator.Send(new CheckLetter(letter));
            return Ok(result);
        }
    }
}
