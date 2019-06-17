using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Sudoku.Logic.Handlers
{
    public class ChangeCellValueHandler : IRequestHandler<ChangeCellValueCommand, Result<Session>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<ChangeCellValueCommand> _validator;

        public ChangeCellValueHandler(SudokuDbContext context, IMapper mapper, IValidator<ChangeCellValueCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Session>> Handle(ChangeCellValueCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request, ruleSet: "IsValidSudokuGameSet");

            if (result.Errors.Count > 0)
            {
                return Result.Fail<Session>(result.Errors.First().ErrorMessage);
            }

            var session = _context.Sessions
                .Include(d => d.SquaresDb)
                .First(d => d.Id == request.Id);
            session.SquaresDb.First(x => x.X == request.X && x.Y == request.Y).Value = request.Value;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Result.Ok<Session>(_mapper.Map<Session>(session));
        }


    }
}
