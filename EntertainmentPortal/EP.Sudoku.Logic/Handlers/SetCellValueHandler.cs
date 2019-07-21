using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Sudoku.Logic.Handlers
{
    public class SetCellValueHandler : IRequestHandler<SetCellValueCommand, Result<Session>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<SetCellValueCommand> _validator;

        public SetCellValueHandler(SudokuDbContext context, IMapper mapper, IValidator<SetCellValueCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<Session>> Handle(SetCellValueCommand request, CancellationToken cancellationToken)
        {
            await AddScore(request, cancellationToken);

            var result = _validator.Validate(request, ruleSet: "IsValidSudokuGameSet");

            if (result.Errors.Count > 0)
            {
                await AddError(request, cancellationToken);
                return Result.Fail<Session>(result.Errors.First().ErrorMessage);
            }

            var sessionDb = _context.Sessions
                .Include(d => d.SquaresDb)
                .First(d => d.Id == request.SessionId);
            sessionDb.SquaresDb.First(x => x.Id == request.Id).Value = request.Value;
            sessionDb.IsOver = IsOver(sessionDb.SquaresDb);

            if (sessionDb.IsOver)
            {
                var playerDb = _context.Players
                    .Include(d => d.GameSessionDb)
                    .First(d => d.GameSessionDb.Id == request.SessionId);
                playerDb.WonGames++;
                if (playerDb.BestResult > sessionDb.Score || playerDb.BestResult == 0)
                {
                    playerDb.BestResult = sessionDb.Score;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Session>(_mapper.Map<Session>(sessionDb));
        }

        public async Task<bool> AddScore(SetCellValueCommand request, CancellationToken cancellationToken)
        {
            var sessionDbScore = _context.Sessions
                .First(d => d.Id == request.SessionId);
            sessionDbScore.Score++;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> AddError(SetCellValueCommand request, CancellationToken cancellationToken)
        {
            var sessionDbScore = _context.Sessions
                .First(d => d.Id == request.SessionId);
            sessionDbScore.Error++;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public bool IsOver(List<CellDb> cells)
        {
            foreach (CellDb cell in cells)
            {
                if (cell.Value == 0)
                {
                    return false;
                }
            }

            return true;
        }


    }
}
