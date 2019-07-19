using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EP.TicTacToe.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(context.Exception.Message)
                { StatusCode = StatusCodes.Status500InternalServerError };
            _logger.LogError($"{context.Exception.Message}");
        }
    }
}