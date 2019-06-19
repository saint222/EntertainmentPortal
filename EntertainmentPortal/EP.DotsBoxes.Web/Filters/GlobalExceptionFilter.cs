using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.DotsBoxes.Web.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            context.Result = new ContentResult
            {
                Content = $"{context.ActionDescriptor.DisplayName} {context.Exception.StackTrace} {context.Exception.Message}",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
