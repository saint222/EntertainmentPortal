using EP._15Puzzle.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EP._15Puzzle.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<DeckController> _logger;

        public GlobalExceptionFilter(ILogger<DeckController> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogCritical(context.Exception,"unexpected exception");
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(context.Exception.Message)
                { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}
