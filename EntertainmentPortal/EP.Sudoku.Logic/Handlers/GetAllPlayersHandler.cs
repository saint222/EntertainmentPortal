using EP.Sudoku.Data;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        public Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var items = PlayerStorage.Players.Select(b => new Player()
            {
                Id = b.Id,
                NickName = b.NickName,
                ExperiencePoint = b.ExperiencePoint,
                Level = b.Level,
                AvatarIcon = new AvatarIcon()
                {
                    Id = b.AvatarIconDb.Id,
                    Uri = b.AvatarIconDb.Uri,
                    IsBaseIcon = b.AvatarIconDb.IsBaseIcon
                }                
            }).ToArray();

            return Task.FromResult((IEnumerable<Player>)items);
        }
    }
}
