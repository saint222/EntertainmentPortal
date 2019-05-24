using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace EP.Sudoku.Logic.Tests
{
    [TestFixture]
    public class ServicesTests
    {
        private readonly Services.Services _services = new Services.Services();

        [Test]
        public void Test_Transposition_Method()
        {
            int[,] matrix = _services.GetBaseMatrix();

            _services.Transposition(matrix);
            _services.Transposition(matrix);

            Assert.AreEqual(_services.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsSmall_Method()
        {
            int[,] matrix = _services.GetBaseMatrix();
            OutputWrite(matrix);
            _services.SwapRowsSmall(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_services.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsSmall_Method()
        {
            int[,] matrix = _services.GetBaseMatrix();
            OutputWrite(matrix);
            _services.SwapColumnsSmall(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_services.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapRowsArea_Method()
        {
            int[,] matrix = _services.GetBaseMatrix();
            OutputWrite(matrix);
            _services.SwapRowsArea(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_services.GetBaseMatrix(), matrix);
        }

        [Test]
        public void Test_SwapColumnsArea_Method()
        {
            int[,] matrix = _services.GetBaseMatrix();
            OutputWrite(matrix);
            _services.SwapColumnsArea(matrix);
            OutputWrite(matrix);

            Assert.AreNotEqual(_services.GetBaseMatrix(), matrix);
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
