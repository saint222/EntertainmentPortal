using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
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

        [HttpGet("api/cell")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Cell), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Cell not found")]
        public async Task<IActionResult> GetCellAsync(int x, int y)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: x = {x}, y = {y}");

            var result = await _mediator.Send(new GetCell(x, y)).ConfigureAwait(false);
            return result.HasValue ? (IActionResult)Ok(result.Value) : BadRequest();
        }

        [HttpPost("api/cell")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(Cell), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description =
            "Invalid data")]
        public async Task<IActionResult> PostCell([FromRoute]long mapId, [FromBody]Cell cell)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: mapId = {mapId}, Cell: X = {cell.X}, Y = {cell.Y}, Letter = {cell.Letter}");

            var result = await _mediator.Send(new AddLetterCommand
            {
                MapId = mapId,
                X = cell.X,
                Y = cell.Y,
                Letter = cell.Letter
            });

            return result != null ? (IActionResult) Ok(result) : BadRequest();
        }
    }
}