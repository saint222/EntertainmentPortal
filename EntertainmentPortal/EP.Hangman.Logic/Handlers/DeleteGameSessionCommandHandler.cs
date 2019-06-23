using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Context;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.Hangman.Logic.Handlers
{
    public class DeleteGameSessionCommandHandler : IRequestHandler<DeleteGameSessionCommand, Result<ControllerData>>
    {
        private readonly GameDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteGameSessionCommand> _validator;
        private readonly ILogger<DeleteGameSessionCommandHandler> _logger;

        public DeleteGameSessionCommandHandler(GameDbContext context, IMapper mapper, IValidator<DeleteGameSessionCommand> validator, ILogger<DeleteGameSessionCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<ControllerData>> Handle(DeleteGameSessionCommand request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                _logger.LogError("Request is invalid");
                return Result.Fail<ControllerData>(validator.Errors.First().ErrorMessage);
            }

            var result = await _context.Games.FindAsync(request._data.Id);
            if (result == null)
            {
                _logger.LogError($"Game session {request._data.Id} wasn't found");
                return Result.Fail<ControllerData>("Data wasn't found");
            }

            _context.Entry<GameDb>(result).State = EntityState.Deleted;

            try
            {
                _logger.LogInformation("Updating database");
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _logger.LogInformation("Database was updated");

                return Result.Ok<ControllerData>(null);
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError("Unsuccessful database update");
                return Result.Fail<ControllerData>(exception.Message);
            }
        }
    }
}
