using System;
using System.Threading.Tasks;
using EP.TicTacToe.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EP.TicTacToe.Web.Filters
{
    public class GlobalActionFilter : IActionFilter, IAsyncActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context,
                                           ActionExecutionDelegate next)
        {
            if (context.Controller is GameController)
            {
            }

            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ModelState);

            context.Result = new ContentResult {Content = "Filter Executed"};
            return Task.CompletedTask;
        }
    }
}