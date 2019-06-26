using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetPlayer : IRequest<Maybe<PlayerDb>>
    {
        public int Id { get; set; }

        public GetPlayer(int id)
        {
            Id = id;
        }
    }
}
