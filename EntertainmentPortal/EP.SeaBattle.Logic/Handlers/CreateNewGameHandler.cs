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
using System.Collections.Generic;
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
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid)
            {
                _context.Games.Add(_mapper.Map<GameDb>(request));
                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    _logger.LogInformation($"Game created. Player1 {request.Player1.NickName}, Player2 {request.Player2.NickName}");
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
