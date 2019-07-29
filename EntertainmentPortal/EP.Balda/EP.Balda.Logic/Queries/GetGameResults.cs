using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetGameResults : IRequest<Maybe<Game>>
    {
        public string PlayerId { get; set; }
    }
}