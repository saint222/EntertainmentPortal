using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EP.WordsMaker.Data;
using EP.WordsMaker.Logic.Queries;
using EP.WordsMaker.Logic.Models;

namespace EP.WordsMaker.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        public Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var items = PlayerStorage.Players.Select(p => new Player()
                                                    {
                                                        Id = p.Id,
                                                        Name = p.Name,
                                                        Score = p.Score
                                                    }).ToArray();
            return Task.FromResult((IEnumerable<Player>) items);                                                                                                             
        }
    }
}
