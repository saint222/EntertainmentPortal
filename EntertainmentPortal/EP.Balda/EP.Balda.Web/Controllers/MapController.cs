using System;
using System.Net;
using System.Threading.Tasks;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using EP.Balda.Web.Models;
using EP.Balda.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace EP.Balda.Web.Controllers
{
    [ApiController]
    public class MapController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MapController> _logger;

        public MapController(IMediator mediator, ILogger<MapController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("api/map")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Map not found")]
        public async Task<IActionResult> GetMapAsync([FromQuery] long id)
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: id = {id}");

            var result = await _mediator.Send(new GetMap() { Id = id });

            if (result.HasValue)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                $"Parameter: Id = {id}");

                //return Ok(result.Value);

                var cells = Helpers.Do2DimArray(result.Value);
                return Ok(cells);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                    $"Id = {id} - Map not found");

                return NotFound();
            }
        }

        [HttpGet("api/map/alphabet")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Game), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Error")]
        public IActionResult GetAlphabet()
        {
            _logger.LogDebug(
                $"Action: {ControllerContext.ActionDescriptor.ActionName}");

            var alphabet = Helpers.GetEnglishAlphabet();

            return alphabet != null ? (IActionResult)Ok(alphabet) : BadRequest();

        }

        [HttpPut("api/map/word")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Player), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddWordAsync([FromBody] GameAndCells gameAndCells)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
                             $"Parameters: gameId = {gameAndCells.GameId}");

            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return BadRequest("User is not authorized");
            }

            var model = new AddWordToPlayerCommand();
            model.PlayerId = UserId;
            model.GameId = gameAndCells.GameId;
            model.CellsThatFormWord = gameAndCells.CellsThatFormWord;

            var result = await _mediator.Send(model);

            string word = Helpers.FormWordFromCells(gameAndCells.CellsThatFormWord);

            
            if (result.IsSuccess)
            {
                _logger.LogInformation(
                    $"Action: {ControllerContext.ActionDescriptor.ActionName} : - " +
                    $"The word {word} was written at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                var array = Helpers.Do2DimArray(result.Value);

                return Ok(array);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: " +
                $"Id = {model.PlayerId} - Word can't be written");

                return BadRequest("Word " + word + ": " + result.Error);
            }
        }
    }
}