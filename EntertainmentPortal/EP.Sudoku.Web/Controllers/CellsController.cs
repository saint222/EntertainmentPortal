using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace EP.Sudoku.Web.Controllers
{
    [ApiController]
    public class CellsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CellsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("api/cell")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(void), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> EditPlayer([FromBody]Cell model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var player = await _mediator.Send(new UpdateCell(model));
            return true ? (IActionResult)Ok() : NotFound();
        }
    }
}