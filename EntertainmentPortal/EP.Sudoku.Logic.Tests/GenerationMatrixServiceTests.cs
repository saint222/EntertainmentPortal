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
            _service.SwapRowsSmall(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsSmall_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            _service.SwapColumnsSmall(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsArea_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            _service.SwapRowsArea(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsArea_Method()
        {
            int[,] matrix = _service.GetBaseMatrix();
            _service.SwapColumnsArea(matrix);

            Assert.AreNotEqual(_service.GetBaseMatrix(), matrix);
        }
    }
}