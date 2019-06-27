using EP.Sudoku.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;

namespace EP.Sudoku.Logic.Commands
{
    public class UpdatePlayerCommand : IRequest<Result<Player>>
    {
        public Player player { get; set; }
        public UpdatePlayerCommand(Player player)
        {
            this.player = player;
        }
    }
}
