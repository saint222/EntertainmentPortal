using NUnit.Framework;

namespace EP.Sudoku.Logic.Tests
{
    [TestFixture]
    public class SolverServiceTests
    {
        private readonly Services.GenerationMatrixService _generationMatrix = new Services.GenerationMatrixService();
        private readonly Services.SolverService _solver = new Services.SolverService();

        [Test]
        public void Test_Solver_Method()
        {
            int[,] matrix = _generationMatrix.GetBaseMatrix();
            matrix = _generationMatrix.RemoveCells(matrix, 50);
            matrix = _solver.Solver(matrix);

            //Assert.
        }
    }
}
