using CSharpFunctionalExtensions;
using EP.Balda.Data.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetGame : IRequest<Maybe<GameDb>>
    {
        public long Id { get; set; }
    }
}