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
    public class WordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Word
        [HttpGet]
        public async Task<ActionResult> GetAllWordsAsync()
        {
            var result = await _mediator.Send(new GetAllWords());

            return Ok(result);
        }
    }
}
