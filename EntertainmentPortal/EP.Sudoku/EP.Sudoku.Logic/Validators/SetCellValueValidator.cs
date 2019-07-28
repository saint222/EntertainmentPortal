using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Solver = SudokuSolver.SudokuSolver;

namespace EP.Sudoku.Logic.Validators
{
    public class SetCellValueValidator : AbstractValidator<SetCellValueCommand>
    {
        private const int GRID_DIMENSION = 9;
        private readonly SudokuDbContext _context;

        public SetCellValueValidator(SudokuDbContext context)
        {
            _context = context;

            RuleSet("PreValidationCell", () =>
            {
                RuleFor(x => x.Value)
                    .InclusiveBetween(1, 9)
                    .WithMessage("Numbers with values from 1 to 9 can be used only!");
            });

            RuleSet("IsValidSudokuGameSet", () =>
            {                
                RuleFor(x => x)
                    .Must((o, token) =>
                    {
                        var session = GetSession(o);
                        var result = Solver.IsValidSudokuGame(CellsToGrid(session.Result.SquaresDb));

                        return result ? true : false;
                    })
                    .WithMessage($"Incorrect value. Think it over again due to the game rules!");

                RuleFor(x => x)
                    .Must( (o, token) => 
                        {
                            var session = GetSession(o);
                            var result = false;
                            if(Solver.IsValidSudokuGame(CellsToGrid(session.Result.SquaresDb)))
                                result = Solver.Solve(CellsToGrid(session.Result.SquaresDb));

                            return result ? true : false;
                        })
                    .WithMessage($"Incorrect value. Think it over again due to the game rules!");
            });
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

        private async Task<SessionDb> GetSession(SetCellValueCommand model)
        {
            var session = await _context.Sessions
                .Include(d => d.SquaresDb)
                .FirstAsync(d => d.Id == model.SessionId);

            if (session != null)
            {
                session.SquaresDb.First(x => x.Id == model.Id).Value = model.Value;
            }

            return session;
        }
    }
}
