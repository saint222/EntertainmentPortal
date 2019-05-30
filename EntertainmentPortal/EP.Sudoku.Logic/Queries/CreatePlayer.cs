using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Queries
{
    public class CreatePlayer : IRequest<Player>
    {
        public Player player { get; set; }
        public CreatePlayer(Player p)
        {
            player = p;
        }
    }
}
