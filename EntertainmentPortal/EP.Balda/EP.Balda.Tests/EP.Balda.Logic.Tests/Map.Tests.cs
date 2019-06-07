using EP.Balda.Logic.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace EP.Balda.Tests
{
    [TestFixture]
    public class MapTests
    {
        private Map _map;

        [SetUp]
        public void Setup()
        {
            _map = new Map(5);
            _map.Fields[1, 1].Letter = 'c';
            _map.Fields[2, 1].Letter = 'a';
            _map.Fields[3, 1].Letter = 't';
        }

        [Test]
        public void TestGetCell()
        {
            var result = _map.GetCell(1, 1);
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestIsEmptyCellTrue()
        {
            var result = _map.IsEmptyCell(0, 1);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsEmptyCellFalse()
        {
            var result = _map.IsEmptyCell(1, 1);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestIsAllowedCellTrue()
        {
            var result = _map.IsAllowedCell(2, 2);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsAllowedCellFalse()
        {
            var result = _map.IsAllowedCell(4, 4);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestIsItCorrectWordTrue()
        {
            List<Cell> cells = new List<Cell>
            {
                _map.Fields[1, 1],
                _map.Fields[2, 1],
                _map.Fields[3, 1]
            };

            var result = _map.IsItCorrectWord(cells);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsItCorrectWordFalse()
        {
            List<Cell> cells = new List<Cell>
            {
                _map.Fields[1, 1],
                _map.Fields[3, 1]
            };

            var result = _map.IsItCorrectWord(cells);
            Assert.IsFalse(result);
        }

        [Test]
        public void TestGetSelectedWord()
        {
            List<Cell> cells = new List<Cell>
            {
                _map.Fields[1, 1],
                _map.Fields[2, 1],
                _map.Fields[3, 1]
            };

            var result = _map.GetSelectedWord(cells);
            Assert.AreEqual("cat", result);
        }
    }
}
