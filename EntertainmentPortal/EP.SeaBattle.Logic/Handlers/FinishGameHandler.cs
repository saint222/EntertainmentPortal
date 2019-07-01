using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using EP.SeaBattle.Data.Models;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class FinishGameHandler : IRequestHandler<FinishGameCommand, Result<Game>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FinishGameHandler(SeaBattleDbContext context, IMapper mapper, ILogger<FinishGameHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Game>> Handle(FinishGameCommand request, CancellationToken cancellationToken)
        {
            PlayerDb player = await _context.Players.FindAsync(request.PlayerId).ConfigureAwait(false);
            GameDb game = await _context.Games.FirstOrDefaultAsync(g => g.Id == player.Game.Id).ConfigureAwait(false);
            if (game == null)
            {
                _logger.LogWarning($"Game with id {player.Game.Id} not found");
                return Result.Fail<Game>("Game not found");
            }

            game.Finish = true;
            game.PlayerAllowedToMove = null;
            game.Players.Remove(player);

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation($"Player {game.Finish} changed");
                return Result.Ok<Game>(_mapper.Map<Game>(game));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail<Game>("Cannot update game");
            }
        }
    }
}


