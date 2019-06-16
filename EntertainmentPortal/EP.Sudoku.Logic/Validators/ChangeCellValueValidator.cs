using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Solver = SudokuSolver.SudokuSolver;

namespace EP.Sudoku.Logic.Validators
{
    public class ChangeCellValueValidator : AbstractValidator<ChangeCellValueCommand>
    {
        private const int GRID_DIMENSION = 9;
        private readonly SudokuDbContext _context;

        public ChangeCellValueValidator(SudokuDbContext context)
        {
            _context = context;

            RuleSet("IsValidSudokuGameSet", () =>
            {
                RuleFor(x => x)
                    .MustAsync(
                        async (o, token) =>
                        {
                            var session = GetSession(o);
                            var result = Solver.IsValidSudokuGame(CellsToGrid(session.Result.SquaresDb));

                            return result ? true : false;
                        })
                    .WithMessage("Numbers with values from 1 to 9 can be used only!");

                RuleFor(x => x)
                    .MustAsync(
                        async (o, token) =>
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

        private async Task<SessionDb> GetSession(ChangeCellValueCommand model)
        {
            var session = _context.Sessions
                .Include(d => d.SquaresDb)
                .First(d => d.Id == model.Id);
            session.SquaresDb.Where(x => x.X == model.X && x.Y == model.Y).FirstOrDefault().Value = model.Value;

            return session;
        }
    }
}
