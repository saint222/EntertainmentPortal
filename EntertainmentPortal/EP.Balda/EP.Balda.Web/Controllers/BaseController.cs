using EP.Balda.Web.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EP.Balda.Web.Controllers
{
    [GlobalExceptionFilter]
    [Authorize]
    public class BaseController : ControllerBase
    {
        protected string UserId => ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}