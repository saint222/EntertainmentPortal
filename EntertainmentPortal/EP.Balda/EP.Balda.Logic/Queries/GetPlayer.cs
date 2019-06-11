using CSharpFunctionalExtensions;
using EP.Balda.Data.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetPlayer : IRequest<Maybe<PlayerDb>>
    {
        public long Id { get; set; }

        public GetPlayer(long id)
        {
            Id = id;
        }
    }
}