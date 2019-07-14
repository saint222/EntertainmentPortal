using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Web.Hubs
{
    public class SudokuHub : Hub
    {
        private readonly ILogger<SudokuHub> _logger;

        public SudokuHub(ILogger<SudokuHub> logger)
        {
            _logger = logger;
        }

        public Task CalledFromClient(string msg)
        {
            _logger.LogCritical($"Message: {msg}.");            
            return Task.CompletedTask;
        }
    }
}
