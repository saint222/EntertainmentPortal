using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EP.Sudoku.Logic.Models;

namespace EP.Sudoku.Web.Hubs
{
    /// <summary>
    /// Class SudokuHub for SingleR usage.
    /// </summary>
    public class SudokuHub : Hub
    {
        private readonly ILogger<SudokuHub> _logger;

        /// <summary>
        ///SudokuHub constructor.
        /// </summary>
        public SudokuHub(ILogger<SudokuHub> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Catches messages fron UI 
        /// </summary>
        public Task GetMes(ChatMessage chat)
        {
            _logger.LogCritical($"Message: {chat.Name}: {chat.Message}.");            
            return Clients.All.SendAsync("SendMes", chat);                
        }
    }
}
