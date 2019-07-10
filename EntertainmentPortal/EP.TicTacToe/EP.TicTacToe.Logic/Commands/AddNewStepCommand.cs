using CSharpFunctionalExtensions;
using EP.TicTacToe.Logic.Models;
using MediatR;

namespace EP.TicTacToe.Logic.Commands
{
    public class AddNewStepCommand : IRequest<Result<Cell>>
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public int Index { get; set; }

    }
}