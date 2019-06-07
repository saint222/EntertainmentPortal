using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.Sudoku.Web.Controllers
{
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SessionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/sessions")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionCommand model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var session = await _mediator.Send(model);

            return true ? (IActionResult)Ok() : NotFound();
        }
    }
}
