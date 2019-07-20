using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EP.SeaBattle.Logic.Handlers
{
    class StartGameHandler : IRequestHandler<StartGameCommand, Result<Game>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private IValidator<StartGameCommand> _validator;
        private readonly ILogger _logger;

        public StartGameHandler(SeaBattleDbContext context, IMapper mapper, IValidator<StartGameCommand> validator, ILogger<StartGameHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Game>> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "GameValidation", cancellationToken: cancellationToken);
            if (validationResult.IsValid)
            {
                GameDb game = await _context.Games
                    .FirstOrDefaultAsync(g => g.EnemySearch == true).ConfigureAwait(false);
                if (game == null)
                {
                    game = new GameDb
                    {
                        Finish = false,
                        EnemySearch = true
                    };
                    game.Players.Add(await _context.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId));
                    game.PlayerAllowedToMove = request.PlayerId;
                    _context.Games.Add(game);
                }
                else
                {

                    game.Players.Add(await _context.Players.FirstOrDefaultAsync(p => p.Id == request.PlayerId).ConfigureAwait(false));
                    game.EnemySearch = false;
                }




                try
                {
                    await _context.SaveChangesAsync();
                    if (game.EnemySearch == true)
                    {
                        _logger.LogInformation($"The game with id {game.Id} saved and looking for second player");
                    }
                    else
                    {
                        _logger.LogInformation($"The game with id {game.Id} has begun");
                    }

                    return Result.Ok(_mapper.Map<Game>(game));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex.Message);
                    return Result.Fail<Game>("Cannot start the game");
                }
            }
            else
            {
                _logger.LogWarning(string.Join(", ", validationResult.Errors));
                return Result.Fail<Game>(validationResult.Errors.First().ErrorMessage);
            }
        }
    }
}

