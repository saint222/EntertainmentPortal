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
using Microsoft.Extensions.Logging;
using Solver = SudokuSolver.SudokuSolver;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetHintHandler : IRequestHandler<GetHintCommand, Result<Session>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<GetHintCommand> _validator;
        private readonly ILogger<GetHintHandler> _logger;
        private const int GRID_DIMENSION = 9;

        public GetHintHandler(SudokuDbContext context, IMapper mapper, IValidator<GetHintCommand> validator, ILogger<GetHintHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Session>> Handle(GetHintCommand request, CancellationToken cancellationToken)
        {
            var validator = _validator.Validate(request, ruleSet: "IsValidGetHint");

            if (!validator.IsValid)
            {
                _logger.LogError(validator.Errors.First().ErrorMessage);
                return Result.Fail<Session>(validator.Errors.First().ErrorMessage);
            }

            await AddScore(request, cancellationToken);

            var session = _context.Sessions
                .Include(d => d.SquaresDb)
                .First(d => d.Id == request.SessionId);
            session.SquaresDb.First(x => x.Id == request.Id).Value = GetHint(session, request.Id);
            session.Hint--;
            session.IsOver = IsOver(session.SquaresDb);

            if (session.IsOver)
            {
                var playerDb = _context.Players
                    .Include(d => d.GameSessionDb)
                    .First(d => d.GameSessionDb.Id == request.SessionId);
                playerDb.WonGames++;
                if (playerDb.BestResult > session.Score || playerDb.BestResult == 0)
                {
                    playerDb.BestResult = session.Score;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok<Session>(_mapper.Map<Session>(session));
        }

        public async Task<bool> AddScore(GetHintCommand request, CancellationToken cancellationToken)
        {
            var sessionDbScore = _context.Sessions
                .First(d => d.Id == request.SessionId);
            sessionDbScore.Score++;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public int GetHint(SessionDb session, int cellId)
        {
            for (int i = 1; i <= GRID_DIMENSION; i++)
            {
                session.SquaresDb.First(x => x.Id == cellId).Value = i;
                if (Solver.IsValidSudokuGame(CellsToGrid(session.SquaresDb)))
                    if (Solver.Solve(CellsToGrid(session.SquaresDb)))
                        return i;
            }

            return 0;
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

        private int[,] CellsToGrid(List<CellDb> cells)
        {
            int[,] grid = new int[GRID_DIMENSION, GRID_DIMENSION];
            foreach (var cell in cells)
            {
                grid[cell.X - 1, cell.Y - 1] = (int)cell.Value;
            }

            return grid;
        }
    }
}
