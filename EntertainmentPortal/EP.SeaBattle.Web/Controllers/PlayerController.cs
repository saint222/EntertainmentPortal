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
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Add new player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddPlayerAsync([FromBody]AddNewPlayerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsFailure ?
                (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Update player")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> UpdatePlayerAsync([FromBody]UpdatePlayerCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsFailure ?
                (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }
    }
}
