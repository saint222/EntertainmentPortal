using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.SeaBattle.Data.Context;
using EP.SeaBattle.Data.Models;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Models;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<Game>>
    {
        private readonly SeaBattleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateNewGameCommand> _validator;
        private readonly ILogger _logger;

        public CreateNewGameHandler(SeaBattleDbContext context, IMapper mapper, IValidator<CreateNewGameCommand> validator, ILogger<CreateNewGameHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Game>> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, ruleSet: "GameValidation", cancellationToken: cancellationToken);
            if (validationResult.IsValid)
            {
                //TODO спросить, не костыль ли так добавлять в БД запись
                var game = new GameDb()
                {
                    Id = request.GameId,
                    Player1 = await _context.Players.FindAsync(request.PlayerId).ConfigureAwait(false),
                    Status = Common.Enums.GameStatus.NotReady
                };
                _context.Games.Add(_mapper.Map<GameDb>(request));
                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    _logger.LogInformation($"New game created. Player1Id {request.PlayerId}");
                    return Result.Ok<Game>(_mapper.Map<Game>(request));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex.Message);
                    return Result.Fail<Game>("Game not created");
                }
            }
            else
            {
                _logger.LogWarning("Request not valid. Game not created");
                return Result.Fail<Game>(string.Join(", ", validationResult.Errors));
            }
        }
    }
}
