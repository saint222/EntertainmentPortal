using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetPlayer : IRequest<Maybe<Player>>
    {
        public string Id { get; set; }
    }
}