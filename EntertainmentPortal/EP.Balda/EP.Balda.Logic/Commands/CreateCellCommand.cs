using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class CreateCellCommand : IRequest<Result<Cell>>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char? Letter { get; set; }
    }
}