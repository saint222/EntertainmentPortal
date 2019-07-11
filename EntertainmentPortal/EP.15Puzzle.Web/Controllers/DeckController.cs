using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetDeckQuery(User.FindFirst(c => c.Type == "sub").Value));
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound("Start page");
        }

        // POST: api/Deck
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Post()
        {
            var result = await _mediator.Send(new NewDeckCommand(User.FindFirst(c => c.Type == "sub").Value));
            if (result.IsSuccess)
            {
                return (IActionResult)Ok(result.Value);
            }

            return NotFound(result.Error);
        }



        // PUT: api/Deck
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Deck), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Put([FromBody][NotNull] int tile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new MoveTileCommand(User.FindFirst(c => c.Type == "sub").Value, tile));
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);

        }

       
        
    }
    
}
