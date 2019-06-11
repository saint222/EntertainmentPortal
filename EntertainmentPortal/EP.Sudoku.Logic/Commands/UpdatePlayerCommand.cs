using EP.Sudoku.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Commands
{
    public class UpdatePlayerCommand : IRequest<Player>
    {
        public Player player { get; set; }
        public UpdatePlayerCommand(Player p)
        {
            player = p;
        }
    }
}
