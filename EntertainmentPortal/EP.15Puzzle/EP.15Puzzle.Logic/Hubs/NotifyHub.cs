using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EP._15Puzzle.Logic.Hubs
{
    public class NotifyHub: Hub
    {
        public async Task Notice(string message)
        {
            await this.Clients.All.SendAsync("notice", message);
        }
    }
}
