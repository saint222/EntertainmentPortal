using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP.SeaBattle.Data.Storages;
using EP.SeaBattle.Logic.Models;
using EP.SeaBattle.Logic.Queries;
using MediatR;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        public Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var items = PlayersStorage.Players.Select(s => new Player()
            {
                Id = s.Id,
                Login = s.Login,
                IsBanned = s.IsBanned,
                BanExpire = s.BanExpire
            }).ToArray();

            return Task.FromResult((IEnumerable<Player>)items);
        }
    }
}
