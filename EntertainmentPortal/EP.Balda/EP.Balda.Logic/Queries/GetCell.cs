using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetCell : IRequest<Maybe<Cell>>
    {
        public long Id { get; set; }
    }
}