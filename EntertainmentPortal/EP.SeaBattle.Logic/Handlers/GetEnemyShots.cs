using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Queries;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class GetEnemyShotsHandler : IRequestHandler<GetEnemyShotsQuery, Maybe<IEnumerable<Shot>>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEnemyShotsQuery> _logger;

        public GetEnemyShotsHandler(SeaBattleDbContext context, IMapper mapper, ILogger<GetEnemyShotsQuery> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Maybe<IEnumerable<Shot>>> Handle(GetEnemyShotsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.UserId))
            {
                GameDb gameDb = await _context.Games.Where(g => g.Players.Where(p => p.UserId == request.UserId).Count() > 0).FirstOrDefaultAsync().ConfigureAwait(false);
                PlayerDb enemyPlayerDb = await _context.Players
                                                       .FirstOrDefaultAsync(p => p.GameId == gameDb.Id && p.UserId != request.UserId)
                                                       .ConfigureAwait(false);
               

                IEnumerable<ShotDb> shotsDb = await _context.Shots.Where(w => w.Player.UserId == enemyPlayerDb.UserId)
                    .ToArrayAsync(cancellationToken).ConfigureAwait(false);

                _logger.LogInformation($"Send enemy shot collection for player");
                return shotsDb.Any() ? Maybe<IEnumerable<Shot>>.From(_mapper.Map<IEnumerable<Shot>>(shotsDb))
                    : Maybe<IEnumerable<Shot>>.None;
            }
            return Maybe<IEnumerable<Shot>>.None;
        }
    }
}