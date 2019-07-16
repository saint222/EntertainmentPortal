using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EP.TicTacToe.Web.Filters
{
    public class ValidationFilterAttribute: ActionFilterAttribute

    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                base.OnActionExecuting(context);
            }

        }
    }
}