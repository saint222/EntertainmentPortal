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

namespace EP.Hangman.Logic.Handlers
{
    public class DeleteGameSessionCommandHandler : IRequestHandler<DeleteGameSessionCommand, Result<ControllerData>>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        private readonly IValidator<DeleteGameSessionCommand> _validator;

        public DeleteGameSessionCommandHandler(GameDbContext context, IMapper mapper, IValidator<DeleteGameSessionCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ControllerData>> Handle(DeleteGameSessionCommand request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                return Result.Fail<ControllerData>(validator.Errors.First().ErrorMessage);
            }

            var result = new GameDb(){Id = request._data.Id};

            _context.Entry<GameDb>(result).State = EntityState.Deleted;

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok<ControllerData>(null);
            }
            catch (DbUpdateException exception)
            {
                return Result.Fail<ControllerData>(exception.Message);
            }
        }
    }
}
