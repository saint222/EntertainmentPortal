using NUnit.Framework;
using EP.Sudoku.Logic.Services;

namespace EP.Sudoku.Tests
{
    [TestFixture]
    public class GenerationGridServiceTests
    {
        private readonly GenerationGridService _generationGrid = new GenerationGridService();

        [Test]
        public void Test_Transposition_Method()
        {
            int[,] grid = _generationGrid.GetBaseGrid();
            grid = _generationGrid.Transposition(grid);

            Assert.AreEqual(3, grid[1, 3]);
            Assert.AreEqual(8, grid[5, 6]);
        }

        [Test]
        public void Test_SwapRowsSmall_Method()
        {
            int[,] grid = _generationGrid.GetBaseGrid();;
            _generationGrid.SwapRowsSmall(grid);

            Assert.AreNotEqual(_generationGrid.GetBaseGrid(), grid);
        }

        [Test]
        public void Test_SwapColumnsSmall_Method()
        {
            int[,] grid = _generationGrid.GetBaseGrid();
            _generationGrid.SwapColumnsSmall(grid);

            Assert.AreNotEqual(_generationGrid.GetBaseGrid(), grid);
        }

        [Test]
        public void Test_SwapRowsArea_Method()
        {
            int[,] grid = _generationGrid.GetBaseGrid();
            _generationGrid.SwapRowsArea(grid);

            Assert.AreNotEqual(_generationGrid.GetBaseGrid(), grid);
        }

        [Test]
        public void Test_SwapColumnsArea_Method()
        {
            int[,] grid = _generationGrid.GetBaseGrid();
            _generationGrid.SwapColumnsArea(grid);

            Assert.AreNotEqual(_generationGrid.GetBaseGrid(), grid);
        }

        [Test]
        public void Test_RemoveCells_Method()
        {
            int[,] initGrid = _generationGrid.GetBaseGrid();
            int[,] grid = _generationGrid.RemoveCells(initGrid, 50);

            Assert.AreNotEqual(initGrid, grid);
        }

        [Test]
        public void Test_GetRandomGrid_Method()
        {
            int[,] initGrid = _generationGrid.GetBaseGrid();
            int[,] grid = _generationGrid.GetRandomGrid();

            Assert.AreNotEqual(initGrid, grid);
        }
    }
}