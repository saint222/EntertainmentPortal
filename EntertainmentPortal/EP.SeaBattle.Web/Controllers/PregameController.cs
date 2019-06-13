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
    public class PregameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PregameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //TODO Переделать метод, т.к. добавлять ячейку не целесообразно
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Ship), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddShipAsync([FromBody]AddNewShipCommand model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            return result.HasNoValue ?
                (IActionResult)BadRequest()
                : Ok(result.Value);
        }
    }
}
