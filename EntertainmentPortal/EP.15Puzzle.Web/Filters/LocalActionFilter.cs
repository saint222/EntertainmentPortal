using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EP._15Puzzle.Web.Filters
{
    public class LocalActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = new ContentResult() { Content = "Filter Executed" };
            base.OnActionExecuting(context);
        }
    }
}
