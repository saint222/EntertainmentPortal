using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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

        public Task Subscribe(string msg)
        {
            _logger.LogCritical($"User : {msg} has been subscribed");
            Groups.AddToGroupAsync(Context.ConnectionId, msg);
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
        Task DoSomething2(int code);
    }
}
