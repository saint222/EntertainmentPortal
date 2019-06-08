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


namespace EP.Hangman.Logic.Handlers
{
    public class CheckLetterHandler : IRequestHandler<CheckLetterCommand, Result<ControllerData>>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        private readonly IValidator<CheckLetterCommand> _validator;

        public CheckLetterHandler(GameDbContext context, IMapper mapper, IValidator<CheckLetterCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<ControllerData>> Handle(CheckLetterCommand request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                return Result.Fail<ControllerData>(validator.Errors.First().ErrorMessage);
            }

            var session = await _context.Games.FindAsync(request._data.Id);

            var result = new HangmanGame(_mapper.Map<GameDb, UserGameData>(session)).Play(request._data.Letter);

            var mapped = _mapper.Map<Result<UserGameData>, GameDb>(result);

            _context.Entry<GameDb>(mapped).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                return Result.Ok<ControllerData>(_mapper.Map<Result<UserGameData>, ControllerData>(result));
            }
            catch (DbUpdateException exception)
            {
                return Result.Fail<ControllerData>(exception.Message);
            }
        }
    }
}
