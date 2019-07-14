using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.DotsBoxes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CellController> _logger;

        public CellController(IMediator mediator, ILogger<CellController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET api/cell
        [HttpGet("api/cell")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Cell>), Description = "Received cells")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Cells not found")]
        public async Task<IActionResult> GetCellsAsync()
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName}");
            var result = await _mediator.Send(new GetCells());
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }
    }
}