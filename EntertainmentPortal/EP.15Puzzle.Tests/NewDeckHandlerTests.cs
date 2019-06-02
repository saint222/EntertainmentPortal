using System;
using System.Collections.Generic;
using System.Linq;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Handlers;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NewDeckHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_CheckWin()
        {
            var newDeck = new DeckDb()
            {
                UserId = 1,
                Score = 1,
                Victory = false,
                Tiles = new List<int>() { 16,11,3,8,1,6,10,7,14,5,9,2,4,15,12 }
            };
            
            Assert.Throws<InvalidOperationException>(()=>DeckRepository.Get(newDeck.UserId));
        }

        private bool Equals(DeckDb expected, DeckDb actual)
        {
            return (expected.UserId == actual.UserId &&
                    expected.Score == actual.Score &&
                    expected.Victory == actual.Victory &&
                    expected.Tiles.SequenceEqual(actual.Tiles));

        }
    }
}