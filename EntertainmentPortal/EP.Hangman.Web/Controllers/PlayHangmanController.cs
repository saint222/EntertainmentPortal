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
            return Ok(result);
        }
    }
}
