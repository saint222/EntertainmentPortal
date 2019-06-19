using CSharpFunctionalExtensions;
using EP.Balda.Data.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class AddLetterToCellCommand : IRequest<Result<CellDb>>
    {
        public long Id { get; set; }

        public long MapId { get; set; }

        public char? Letter { get; set; }
    }
}