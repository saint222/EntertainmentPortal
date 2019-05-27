using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace EP.Sudoku.Logic.Tests
{
    [TestFixture]
    public class GenerationMatrixServiceTests
    {
        private readonly Services.GenerationMatrixService _service = new Services.GenerationMatrixService();

        [Test]
        public void Test_Transposition_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();

            _service.Transposition(matrix);
            _service.Transposition(matrix);

            Assert.AreEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsSmall_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            OutputWrite(matrix);
            _service.SwapRowsSmall(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsSmall_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            OutputWrite(matrix);
            _service.SwapColumnsSmall(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsArea_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            OutputWrite(matrix);
            _service.SwapRowsArea(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsArea_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            OutputWrite(matrix);
            _service.SwapColumnsArea(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        public void OutputWrite(int[,] matrix)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Debug.Write(matrix[i, j] + " ");
                }
                Debug.Write(Environment.NewLine);
            }
            Debug.Write(Environment.NewLine);
        }
    }
}
