using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetPlayer : IRequest<Maybe<Player>>
    {
        public int Id { get; set; }

        public GetPlayer(int id)
        {
            Id = id;
        }
    }
}
