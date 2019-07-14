using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Sudoku.Web.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EP.Sudoku.Web.Controllers
{
    [ApiController]
    [Route("api/hub")]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<SudokuHub> _hubContext;

        public HubController(IHubContext<SudokuHub> hubContext)
        {
            _hubContext = hubContext;           
        }

        // GET
        [HttpGet("send")]
        public async Task<IActionResult> SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("getMessage", message);
            return Ok();
        }
    }
}