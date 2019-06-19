using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetMap : IRequest<Maybe<Map>>
    {
        public long Id { get; set; }

        public GetMap(long id)
        {
            Id = id;
        }
    }
}