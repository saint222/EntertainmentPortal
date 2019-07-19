using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EP.TicTacToe.Web.Filters
{
    public class LocalValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;
            context.Result = new BadRequestObjectResult(context.ModelState);
            base.OnActionExecuting(context);
        }
    }
}