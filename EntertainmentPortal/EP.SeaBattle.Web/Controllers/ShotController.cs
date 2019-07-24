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
using FluentValidation;
using NJsonSchema.Annotations;
using FluentValidation.AspNetCore;
using System.ComponentModel;
using Microsoft.AspNetCore.SignalR;

namespace EP.SeaBattle.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShotController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ShotsHub> _hubContext;

        public ShotController(IMediator mediator, IHubContext<ShotsHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("add")]
        [Description("Add shot")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Shot>), Description = "Add ship to player collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't add ship")]
        public async Task<IActionResult> AddShotAsync([FromBody, NotNull, CustomizeValidator(RuleSet = "AddShipPreValidation")] AddShotCommand model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var result = await _mediator.Send(model);
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetOponentShotsAsync(string gameId, string answeredPlayerId)
        {
            var result = await _mediator.Send(new GetShotsQuery() { GameId = gameId, AnsweredPlayerID = answeredPlayerId });
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();
        }
    }
}