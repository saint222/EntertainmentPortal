using System.Linq;
using MediatR;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EP.Hangman.Logic.Handlers
{
    public class CheckLetterHandler : IRequestHandler<CheckLetterCommand, Result<ControllerData>>
    {
        private readonly GameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CheckLetterCommand> _validator;
        private readonly ILogger<CheckLetterHandler> _logger;

        public CheckLetterHandler(GameDbContext context, IMapper mapper, IValidator<CheckLetterCommand> validator, ILogger<CheckLetterHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<ControllerData>> Handle(CheckLetterCommand request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                _logger.LogError("Request is invalid");
                return Result.Fail<ControllerData>(validator.Errors.First().ErrorMessage);
            }

            var session = await _context.Games.FindAsync(request._data.Id);
            if (session == null)
            {
                _logger.LogError($"Game session {request._data.Id} wasn't found");
                return Result.Fail<ControllerData>("Data wasn't found");
            }

            var result = new HangmanGame(_mapper.Map<GameDb, UserGameData>(session)).Play(request._data.Letter);

            var mapped = _mapper.Map<UserGameData, GameDb>(result.Value);

            _context.Entry<GameDb>(mapped).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Updating database");
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation("Database was updated");

                return Result.Ok<ControllerData>(_mapper.Map<UserGameData, ControllerData>(result.Value));
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError("Unsuccessful database update");
                return Result.Fail<ControllerData>(exception.Message);
            }
        }
    }
}
