using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.SeaBattle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Get player ships collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't show player ships")]
        public async Task<IActionResult> GetShipsAsync(string gameId, string playerId)
        {
            var result = await _mediator.Send(new GetShipsQuery() { GameId = gameId, PlayerId = playerId });
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();

        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Add ship to player collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't add ship")]
        public async Task<IActionResult> AddShipAsync([FromBody] AddNewShipCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.HasValue ? Ok(result.Value)
                :(IActionResult) Ok();
        }



        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Delete ship from collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't delete ship")]
        public async Task<IActionResult> DeleteShipAsync([FromBody] DeleteShipCommand model)
        {
            var result = await _mediator.Send(model);
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();
        }
    }
}
