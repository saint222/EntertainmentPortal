using System.Threading.Tasks;
using EP.SeaBattle.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EP.SeaBattle.Web.Controllers
{
    [ApiController]
    [Route("api/hub")]
    public class HubController : Controller
    {
        private readonly IHubContext<SeaBattleHub> _hubContext;
        private readonly IHubContext<DemoGenericHub, IDemoGenericHub> _hubGenericContext;

        public HubController(IHubContext<SeaBattleHub> hubContext
            , IHubContext<DemoGenericHub, IDemoGenericHub> hubGenericContext)
        {
            _hubContext = hubContext;
            _hubGenericContext = hubGenericContext;
        }

        // GET
        [HttpGet("send")]
        public async Task<IActionResult> SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("getMessage", message);
            return Ok();
        }

        // GET
        [HttpGet("send-generic")]
        public async Task<IActionResult> SendStatus(int status)
        {
            await _hubGenericContext.Clients.All.DoSomething2(status);
            return Ok();
        }
    }
}