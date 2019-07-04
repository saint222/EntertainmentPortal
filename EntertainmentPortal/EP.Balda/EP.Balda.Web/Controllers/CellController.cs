using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

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
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Cell not found")]
        public async Task<IActionResult> GetCellAsync([FromQuery] GetCell model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} " +
            $"Parameters: Id = {model.Id}");

            var result = await _mediator.Send(model);

            if (result.HasValue)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameter: Id = {model.Id}");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: Id = {model.Id} - Cell not found");

                return NotFound();
            }
        }

        [HttpPut("api/cell")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Cell), Description = "Success")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Invalid data")]
        public async Task<IActionResult> AddLetterToCellAsync([FromBody] AddLetterToCellCommand model)
        {
            _logger.LogDebug($"Action: {ControllerContext.ActionDescriptor.ActionName} Parameters: Id = {model.Id}, Letter = {model.Letter}");

            var result = await _mediator.Send(model);

            if (result.IsSuccess)
            {
                _logger.LogInformation($"Action: {ControllerContext.ActionDescriptor.ActionName} : Letter {model.Letter} was written at {DateTime.UtcNow} [{DateTime.UtcNow.Kind}]");

                return Ok(result.Value);
            }
            else
            {
                _logger.LogWarning($"Action: {ControllerContext.ActionDescriptor.ActionName}: Id = {model.Id}, Letter = {model.Letter}) - Letter can't be written");

                return BadRequest(result.Error);
            }
        }
    }
}