using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Interfaces;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace EP.Sudoku.Logic.Handlers
{
    public class PostPlayerHandler : IRequestHandler<PostPlayer, Player>
    {

        public Task<Player> Handle(PostPlayer request, CancellationToken cancellationToken)
        {
            var player = new Player
            {
                Id = new PlayerDb().Id,
                NickName = new PlayerDb().NickName,
                ExperiencePoint = new PlayerDb().ExperiencePoint,
                Level = new PlayerDb().Level
            };
            player.AvatarIcon.Id = new AvatarIconDb().Id;
            player.AvatarIcon.Uri = new AvatarIconDb().Uri;

            return Task.FromResult(player);
        }
    }
}
