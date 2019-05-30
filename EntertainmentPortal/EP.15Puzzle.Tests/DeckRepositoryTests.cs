using System;
using System.Collections.Generic;
using System.Linq;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DeckRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Get_Deck_With_Id_0()
        {
            var actual = DeckRepository.Get(0);
            var expected = new DeckDB()
            {
                UserId = 0,
                Score = 0,
                Victory = false,
                Tiles = new List<int>() {16, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
            };
            Assert.IsTrue(Equals(expected, actual));
            
        }
        
        [Test]
        public void Test_Create_Deck_With_Id_1()
        {
            var expected = new DeckDB()
            {
                UserId = 1,
                Score = 5,
                Victory = false,
                Tiles = new List<int>() { 15, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 }
            };
            var newDeck = new DeckDB()
            {
                UserId = 1,
                Score = 5,
                Victory = false,
                Tiles = new List<int>() { 15, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 }
            };
            DeckRepository.Create(newDeck);
            Assert.IsTrue(Equals(expected, DeckRepository.Get(expected.UserId)));
        }
        [Test]
        public void Test_Update_Deck_With_Id_0()
        {
            var expected = new DeckDB()
            {
                UserId = 0,
                Score = 5,
                Victory = false,
                Tiles = new List<int>() { 15, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 }
            };
            var updateDeck = new DeckDB()
            {
                UserId = 0,
                Score = 5,
                Victory = false,
                Tiles = new List<int>() { 15, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 }
            };
            DeckRepository.Update(updateDeck);
            Assert.IsTrue(Equals(expected, DeckRepository.Get( expected.UserId)));
        }

        [Test] public void Test_Delete_Deck_With_Id_5()
        {
            var newDeck = new DeckDB()
            {
                UserId = 5,
                Score = 5,
                Victory = false,
                Tiles = new List<int>() { 15, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 }
            };
            DeckRepository.Create(newDeck);
            DeckRepository.Delete(newDeck);
            Assert.Throws<InvalidOperationException>(()=>DeckRepository.Get(newDeck.UserId));
        }

        private bool Equals(DeckDB  expected, DeckDB actual)
        {
            return (expected.UserId == actual.UserId &&
                    expected.Score == actual.Score &&
                    expected.Victory == actual.Victory &&
                    expected.Tiles.SequenceEqual(actual.Tiles));

        }
    }
}