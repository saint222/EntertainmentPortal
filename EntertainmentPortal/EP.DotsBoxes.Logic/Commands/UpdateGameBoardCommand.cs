using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class UpdateGameBoardCommand : IRequest<Result<GameBoard>>
    {
        public int GameBoardId { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public bool Top { get; set; }

        public bool Bottom { get; set; }

        public bool Left { get; set; }

        public bool Right { get; set; }

        public string Name { get; set; }
    }
}