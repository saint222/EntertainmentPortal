using CSharpFunctionalExtensions;
using EP.Balda.Data.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetMap : IRequest<Maybe<MapDb>>
    {
        public long Id { get; set; }
    }
}