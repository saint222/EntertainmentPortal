using EP.Sudoku.Logic.Models;
using MediatR;
using CSharpFunctionalExtensions;

namespace EP.Sudoku.Logic.Commands
{
    public class CreatePlayerCommand : IRequest<Result<Player>>
    {
        public Player player { get; set; }
        public CreatePlayerCommand(Player player)
        {
            this.player = player;
        }
    }
}
