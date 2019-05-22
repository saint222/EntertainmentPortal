using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Queries;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        public Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var items = PlayerStatistics.Players.Select(b => new Player()
            {
                Id = b.Id,
                Name = b.Name,
                Color = b.Color,
                Score = b.Score
            }).ToArray();

            return Task.FromResult((IEnumerable<Player>)items);
        }
    }
}
