using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class NewGameBoardCommand : IRequest<Result<GameBoard>>
    {
        public int Rows { get; set; }

        public int Columns { get; set; }
    }
}
