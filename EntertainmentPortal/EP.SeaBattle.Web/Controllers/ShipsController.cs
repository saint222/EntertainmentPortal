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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace EP.SeaBattle.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ShipsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShipsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/AddShip")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Add ship to player collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't add ship")]
        public async Task<IActionResult> AddShipAsync([FromBody, NotNull, CustomizeValidator(RuleSet = "AddShipPreValidation")] AddNewShipCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //var accessToken = await HttpContext.GetTokenAsync("access_token"); //Authentication.GetTokenAsync("access_token");
            //var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            //var claims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
            
            var UserID = User.FindFirst("sub")?.Value;
            var UserName = User.FindFirst("name")?.Value;
            var UserEmail = User.FindFirst("email")?.Value;
            var result = await _mediator.Send(model);
            return result.IsFailure
                ? (IActionResult)BadRequest(result.Error)
                : Ok(result.Value);
        }


        [HttpGet("api/GetShips")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Get player ships collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't show player ships")]
        public async Task<IActionResult> GetShipsAsync(string playerId)
        {
            var result = await _mediator.Send(new GetShipsQuery() {PlayerId = playerId });
            return result.HasValue
                ? (IActionResult)Ok(result.Value)
                : NotFound();
        }





        [HttpDelete("api/DeleteShip")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ship>), Description = "Delete ship from collection")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Can't delete ship")]
        public async Task<IActionResult> DeleteShipAsync([FromBody, NotNull, CustomizeValidator(RuleSet = "DeleteShipPreValidation")] DeleteShipCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.HasValue ? Ok(result.Value)
                : (IActionResult)Ok();
        }
    }
}
