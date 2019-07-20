using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.DotsBoxes.Web.Controllers
{
    /// <summary>
    /// This is GameBoardController.
    /// </summary>
    [ApiController]
    public class GameBoardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GameBoardController> _logger;

        /// <summary>
        /// GameBoardController сonstructor. Is used for DI.
        /// </summary>
        public GameBoardController(IMediator mediator, ILogger<GameBoardController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Select a gameboard from the Db.
        /// </summary>
        // GET api/gameboard
        [HttpGet("api/gameboard")]
        [Authorize(AuthenticationSchemes = "Google")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<GameBoard>), Description = "Received game board")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Game board is not created")]
        public async Task<IActionResult> GetGameBoardAsync()
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName}");
            var result = await _mediator.Send(new GetGameBoard());
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.HasValue ? (IActionResult)Ok(result.Value) : NotFound();
        }

        /// <summary>
        /// Сreates a new gameboard according to specified parameters and saves it to the database.
        /// </summary>
        // POST api/gameboard
        [HttpPost("api/gameboard")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(GameBoard), Description = "Create new game board")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> NewGameBoardAsync([FromBody][NotNull]NewGameBoardCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: Game board: " +
                $"Columns = {model.Columns}, Rows = {model.Rows}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: Columns = {model.Columns}, Rows = {model.Rows} - Invalid data");
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(model);
            _logger.LogWarning($"Exit from method: {ControllerContext.ActionDescriptor.ActionName}");

            return result.IsSuccess ? (IActionResult)Ok(result.Value) : BadRequest(result.Error);
        }       
    }
}