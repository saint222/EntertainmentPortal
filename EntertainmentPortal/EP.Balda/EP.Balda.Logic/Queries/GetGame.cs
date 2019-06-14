using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetGame : IRequest<Game>
    {
        public long Id { get; set; }
    }
}