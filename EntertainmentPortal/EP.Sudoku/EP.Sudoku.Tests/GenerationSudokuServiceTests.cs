using System;
using NUnit.Framework;
using EP.Sudoku.Logic.Services;

namespace EP.Sudoku.Tests
{
    [TestFixture]
    public class GenerationSudokuServiceTests
    {
        private readonly GenerationSudokuService _generation = new GenerationSudokuService();
        private readonly int[,] _baseGrid = new int[9, 9] {
            {1, 2, 3, 4, 5, 6, 7, 8, 9},
            {4, 5, 6, 7, 8, 9, 1, 2, 3},
            {7, 8, 9, 1, 2, 3, 4, 5, 6},
            {2, 3, 4, 5, 6, 7, 8, 9, 1},
            {5, 6, 7, 8, 9, 1, 2, 3, 4},
            {8, 9, 1, 2, 3, 4, 5, 6, 7},
            {3, 4, 5, 6, 7, 8, 9, 1, 2},
            {6, 7, 8, 9, 1, 2, 3, 4, 5},
            {9, 1, 2, 3, 4, 5, 6, 7, 8}
        };

        [Test]
        public void Test_Transposition_Method()
        {
            int[,] grid = _baseGrid;
            grid = _generation.Transposition(grid);

            Assert.AreEqual(3, grid[1, 3]);
            Assert.AreEqual(8, grid[5, 6]);
        }

        [Test]
        public void Test_SwapRowsSmall_Method()
        {
            int[,] grid = new int[9, 9];
            Array.Copy(_baseGrid, grid, 81);
            _generation.SwapRowsSmall(grid);

            Assert.AreNotEqual(_baseGrid, grid);
        }

        [Test]
        public void Test_SwapColumnsSmall_Method()
        {
            int[,] grid = new int[9, 9];
            Array.Copy(_baseGrid, grid, 81);
            _generation.SwapColumnsSmall(grid);

            Assert.AreNotEqual(_baseGrid, grid);
        }

        [Test]
        public void Test_SwapRowsArea_Method()
        {
            int[,] grid = new int[9, 9];
            Array.Copy(_baseGrid, grid, 81);
            _generation.SwapRowsArea(grid);

            Assert.AreNotEqual(_baseGrid, grid);
        }

        [Test]
        public void Test_SwapColumnsArea_Method()
        {
            int[,] grid = new int[9,9];
            Array.Copy(_baseGrid, grid, 81);
            _generation.SwapColumnsArea(grid);

            Assert.AreNotEqual(_baseGrid, grid);
        }

        [Test]
        public void Test_RemoveCells_Method()
        {
            int[,] initGrid = new int[9, 9];
            Array.Copy(_baseGrid, initGrid, 81);
            int[,] grid = _generation.RemoveCells(initGrid, 50);

            Assert.AreNotEqual(initGrid, grid);
        }

        [Test]
        public void Test_GetRandomGrid_Method()
        {
            int[,] initGrid = new int[9, 9];
            Array.Copy(_baseGrid, initGrid, 81);
            int[,] grid = _generation.GetRandomGrid();

            Assert.AreNotEqual(initGrid, grid);
        }
    }
}