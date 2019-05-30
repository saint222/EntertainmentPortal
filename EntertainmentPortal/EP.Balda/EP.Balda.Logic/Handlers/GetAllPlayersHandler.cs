using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EP.Balda.Data.EntityFramework;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;

namespace EP.Balda.Logic.Handlers
{
    public class
        GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        public Task<IEnumerable<Player>> Handle(GetAllPlayers request,
                                                CancellationToken cancellationToken)
        {
            var list = new List<Player>();
            foreach (var p in PlayerRepository.Players)
                list.Add(new Player
                {
                    NickName = p.NickName,
                    Login    = p.Login,
                    Password = p.Password,
                    Result   = p.Result
                });

            var items = list.ToArray();

            return Task.FromResult((IEnumerable<Player>) items);
        }
    }
}