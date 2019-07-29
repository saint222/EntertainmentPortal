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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EP.SeaBattle.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace EP.SeaBattle.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ShotsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<SeaBattleHub> _hubContext;

        public ShotsController(IMediator mediator, IHubContext<SeaBattleHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
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
            model.UserId = User.FindFirst("sub")?.Value;
            var result = await _mediator.Send(model);
            await _hubContext.Clients.Group(model.GameId).SendAsync("sendShot",model.UserId, result.Value?.X, result.Value?.Y);
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }

        [HttpGet("api/GetShots")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Shot>), Description = "Get player shots collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't show player shots")]
        public async Task<IActionResult> GetShotsAsync()
        {
            var result = await _mediator.Send(new GetShotsQuery() {UserId = User.FindFirst("sub")?.Value });
            return result.HasValue
                            ? (IActionResult)Ok(result.Value)
                            : NotFound();
        }


        [HttpGet("api/GetEnemyShots")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Shot>), Description = "Get enemy player shots collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't show enemy player shots")]
        public async Task<IActionResult> GetEnemyShots()
        {
            var result = await _mediator.Send(new GetEnemyShotsQuery() { UserId = User.FindFirst("sub")?.Value });
            return result.HasValue
                            ? (IActionResult)Ok(result.Value)
                            : NotFound();
        }

    }
}
