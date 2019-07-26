using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EP._15Puzzle.Web.Hubs
{
    public class NoticeHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.Caller.SendAsync("Notice", message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

    }
}
