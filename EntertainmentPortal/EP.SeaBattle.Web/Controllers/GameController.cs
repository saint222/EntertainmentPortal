using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.SeaBattle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Create new game")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't create a new game")]
        public async Task<IActionResult> CreateGame([FromBody] CreateNewGameCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsSuccess ? Ok(result.Value)
                : (IActionResult)Ok();
        }
    }
}
