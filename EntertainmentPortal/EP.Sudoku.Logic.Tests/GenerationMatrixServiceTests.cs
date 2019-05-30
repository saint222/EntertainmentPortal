using System.Diagnostics;
using NUnit.Framework;

namespace EP.Sudoku.Logic.Tests
{
    [TestFixture]
    public class GenerationMatrixServiceTests
    {
        private readonly Services.GenerationMatrixService _generationMatrix = new Services.GenerationMatrixService();

        [Test]
        public void Test_Transposition_Method()
        {
            int[,] matrix = _generationMatrix.GetBaseMatrix();
            _generationMatrix.Transposition(matrix);
            _generationMatrix.Transposition(matrix);

            Assert.AreEqual(_generationMatrix.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsSmall_Method()
        {
            int[,] matrix = _generationMatrix.GetBaseMatrix();
            _generationMatrix.SwapRowsSmall(matrix);

            Assert.AreNotEqual(_generationMatrix.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsSmall_Method()
        {
            int[,] matrix = _generationMatrix.GetBaseMatrix();
            _generationMatrix.SwapColumnsSmall(matrix);

            Assert.AreNotEqual(_generationMatrix.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsArea_Method()
        {
            int[,] matrix = _generationMatrix.GetBaseMatrix();
            _generationMatrix.SwapRowsArea(matrix);

            Assert.AreNotEqual(_generationMatrix.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsArea_Method()
        {
            int[,] matrix = _generationMatrix.GetBaseMatrix();
            _generationMatrix.SwapColumnsArea(matrix);

            Assert.AreNotEqual(_generationMatrix.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_RemoveCells_Method()
        {
            int[,] initMatrix = _generationMatrix.GetBaseMatrix();
            int[,] matrix = _generationMatrix.RemoveCells(initMatrix, 50);

            Assert.AreNotEqual(initMatrix, matrix);
        }
        
    }
}