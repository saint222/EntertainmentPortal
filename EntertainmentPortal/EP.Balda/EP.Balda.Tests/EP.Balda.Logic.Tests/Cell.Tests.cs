using EP.Balda.Logic.Models;
using NUnit.Framework;

namespace EP.Balda.Tests
{
    [TestFixture]
    public class CellTests
    {
        [Test]
        public void TestIsEmptyTrue()
        {
            Cell cell = new Cell(2, 1);
            bool result = cell.IsEmpty();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsEmptyFalse()
        {
            Cell cell = new Cell(2, 1)
            {
                Letter = 'F'
            };
            bool result = cell.IsEmpty();
            Assert.IsFalse(result);
        }
    }
}
