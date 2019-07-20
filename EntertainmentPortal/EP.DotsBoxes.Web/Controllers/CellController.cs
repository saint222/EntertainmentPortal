using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.DotsBoxes.Web.Controllers
{
    /// <summary>
    /// This is CellController.
    /// </summary>
    [ApiController]
    public class CellController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CellController> _logger;

        /// <summary>
        /// CellController сonstructor. Is used for DI.
        /// </summary>
        public CellController(IMediator mediator, ILogger<CellController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Select a cells from the database.
        /// </summary>
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

        /// <summary>
        /// Changes the cell after the player’s move and saves to the database.
        /// </summary>
        //PUT api/cell
        [HttpPut("api/cell")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Cell), Description = "Add line to cell")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> UpdateCellAsync([FromBody][NotNull]UpdateCellCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: GameBoard (Row = {model.Row}," +
                $" Column = {model.Column}) with cell: Side left = {model.Left}, Side Right = {model.Right}, Side Top = {model.Top}, Side Bottom = {model.Bottom}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: Invalid data");
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.IsSuccess ? (IActionResult)Ok(result) : BadRequest(result.Error);
        }
    }
}