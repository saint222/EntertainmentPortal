using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EP.Sudoku.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _manager;

        public AuthController(UserManager<IdentityUser> manager, IMediator mediator)
        {
            _manager = manager;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]User user)
        {
            return null;
        }
    }
}