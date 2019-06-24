using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class AddLetterToCellCommand : IRequest<Result<Cell>>
    {
        public long Id { get; set; }

        public char? Letter { get; set; }
    }
}