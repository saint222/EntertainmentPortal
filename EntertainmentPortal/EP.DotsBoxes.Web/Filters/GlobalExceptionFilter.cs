using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EP.DotsBoxes.Web.Filters
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
            _logger.LogCritical(context.Exception.Message, "Unexpected exception!");
            context.ExceptionHandled = true;

            context.Result = new ContentResult
            {
                Content = $"{context.ActionDescriptor.DisplayName} {context.Exception.StackTrace} {context.Exception.Message}",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
