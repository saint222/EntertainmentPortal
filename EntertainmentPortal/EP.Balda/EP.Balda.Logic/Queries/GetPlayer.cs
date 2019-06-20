using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetPlayer : IRequest<Maybe<Player>>
    {
        public long Id { get; set; }

        public GetPlayer(long id)
        {
            Id = id;
        }
    }
}