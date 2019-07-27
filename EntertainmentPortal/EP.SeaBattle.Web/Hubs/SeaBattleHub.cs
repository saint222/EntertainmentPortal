using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Web.Hubs
{
    public class SeaBattleHub : Hub
    {
        private readonly ILogger<SeaBattleHub> _logger;

        public SeaBattleHub(ILogger<SeaBattleHub> logger)
        {
            _logger = logger;
        }

        public Task CalledFromClient(string msg, int status)
        {
            _logger.LogCritical($"Message: {msg} with status {status}");
            return Task.CompletedTask;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }

    public class DemoGenericHub : Hub<IDemoGenericHub>
    {
    }

    public interface IDemoGenericHub
    {
        Task DoSomething(int code);
    }
}
