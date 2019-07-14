using System.Linq;
using NUnit.Framework;
using EP.Sudoku.Logic.Services;
using Solver = SudokuSolver.SudokuSolver;

namespace EP.Sudoku.Tests
{
    [TestFixture]
    public class SolverServiceTests
    {
        [Test]
        public void Test_Solve_Method()
        {
            int[,] grid = {
                {0, 0, 0, 0, 0, 0, 0, 6, 8},
                {0, 9, 5, 0, 0, 6, 7, 0, 2},
                {0, 0, 0, 0, 0, 7, 0, 0, 0},
                {0, 0, 0, 0, 4, 5, 3, 0, 0},
                {0, 5, 6, 0, 3, 0, 4, 1, 0},
                {0, 0, 3, 8, 6, 0, 0, 0, 0},
                {0, 0, 0, 5, 0, 0, 0, 0, 0},
                {4, 0, 9, 3, 0, 0, 8, 5, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0}
            };
            bool solved = Solver.Solve(grid);

            Assert.IsTrue(solved);
            Assert.IsTrue(grid.Cast<int>().All(num => num != 0));
            Assert.IsTrue(Solver.IsValidSudokuGame(grid));
        }

        [Test]
        public void Test_Solve_Method_NoSolution()
        {
            int[,] grid = {
                {1, 0, 0, 0, 0, 0, 0, 6, 8},
                {0, 9, 5, 0, 0, 6, 7, 0, 2},
                {0, 0, 0, 0, 0, 7, 0, 0, 0},
                {0, 0, 0, 0, 4, 5, 3, 0, 0},
                {0, 5, 6, 0, 3, 0, 4, 1, 0},
                {0, 0, 3, 8, 6, 0, 0, 0, 0},
                {0, 0, 0, 5, 0, 0, 0, 0, 0},
                {4, 0, 9, 3, 0, 0, 8, 5, 0},
                {5, 2, 0, 0, 0, 0, 0, 0, 0}
            };
            bool solved = Solver.Solve(grid);

            Assert.IsFalse(solved);
        }

        [Test]
        public void Test_Solve_Method_Random()
        {
            GenerationSudokuService generationGrid = new GenerationSudokuService();
            int[,] grid = generationGrid.GetBaseGrid();
            int[,] gridTask = generationGrid.RemoveCells(grid, 50);

            bool solved = Solver.Solve(gridTask);

            Assert.IsTrue(solved);
            Assert.IsTrue(grid.Cast<int>().All(num => num != 0));
            Assert.IsTrue(Solver.IsValidSudokuGame(grid));
        }
    }
}
