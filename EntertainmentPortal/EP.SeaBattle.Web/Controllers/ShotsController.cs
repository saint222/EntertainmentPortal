using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using NJsonSchema.Annotations;
using FluentValidation.AspNetCore;


namespace EP.SeaBattle.Web.Controllers
{
    [ApiController]
    public class ShotsController : Controller
    {
        private readonly IMediator _mediator;

        public ShotsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/HitTarget")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Add ship to player collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't add ship")]
        public async Task<IActionResult> AddShotAsync([FromBody, NotNull, CustomizeValidator(RuleSet = "AddShotPreValidation")] AddShotCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpGet("api/GetShots")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Shot>), Description = "Get player shots collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't show player shots")]
        public async Task<IActionResult> GetShotsAsync(string playerId)
        {
            var result = await _mediator.Send(new GetShotsQuery() {PlayerId = playerId });
            return result.HasValue
                            ? (IActionResult)Ok(result.Value)
                            : NotFound();
        }

    }
}
