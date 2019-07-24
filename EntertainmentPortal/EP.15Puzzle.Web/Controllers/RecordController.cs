using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP._15Puzzle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecordController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/Deck/leaderboard
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Record>), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> Get()
        {

            var result = await _mediator.Send(new GetLeaderboardCommand(HttpContext.Request.Headers["Email"]));
            return result.IsSuccess ? (IActionResult)Ok(result.Value) : NotFound(result.Error);

        }
    }
}