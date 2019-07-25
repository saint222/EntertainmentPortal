using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Handlers;
using EP._15Puzzle.Logic.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NewDeckHandlerTests
    {
        
        [Test]
        public void Test_Create_New_Deck()
        {
            var newDeck = new LogicDeck(4);

            var expectedDeck = new LogicDeck()
            {
                Size = 4,
                Score = 0,
                Victory = false,
                Tiles = new List<LogicTile>()
                {
                    new LogicTile(0,4),
                    new LogicTile(1,4),
                    new LogicTile(2,4),
                    new LogicTile(3,4),
                    new LogicTile(4,4),
                    new LogicTile(5,4),
                    new LogicTile(6,4),
                    new LogicTile(7,4),
                    new LogicTile(8,4),
                    new LogicTile(9,4),
                    new LogicTile(10,4),
                    new LogicTile(11,4),
                    new LogicTile(12,4),
                    new LogicTile(13,4),
                    new LogicTile(14,4),
                    new LogicTile(15,4),
                }

            };

            Assert.IsTrue(EqualsDeck(expectedDeck, newDeck));

        }

        [Test]
        public void Test_Unsort_Deck()
        {
            var newDeck = new LogicDeck(4);
            newDeck.Unsort();
            var expectedDeck = new LogicDeck()
            {
                Size = 4,
                Score = 0,
                Victory = false,
                Tiles = new List<LogicTile>()
                {
                    new LogicTile(0,4),
                    new LogicTile(1,4),
                    new LogicTile(2,4),
                    new LogicTile(3,4),
                    new LogicTile(4,4),
                    new LogicTile(5,4),
                    new LogicTile(6,4),
                    new LogicTile(7,4),
                    new LogicTile(8,4),
                    new LogicTile(9,4),
                    new LogicTile(10,4),
                    new LogicTile(11,4),
                    new LogicTile(12,4),
                    new LogicTile(13,4),
                    new LogicTile(14,4),
                    new LogicTile(15,4),
                }

            };

            Assert.IsFalse(Equals(expectedDeck, newDeck));
        }

        [Test]
        [TestCase(4, 5, ExpectedResult = false)]
        public bool Test_Move_Unmovable_tile_Num_5(int size, int numMoveTile)
        {
            var newDeck = new LogicDeck(size);
            return newDeck.TileCanMove(numMoveTile);
        }

        [Test]
        [TestCase(4, 15, ExpectedResult = true)]
        public bool Test_Move_Movable_tile_Num_15(int size, int numMoveTile)
        {
            var newDeck = new LogicDeck(size);
            return newDeck.TileCanMove(numMoveTile);
        }

        [Test]
        [TestCase(4, 15, ExpectedResult = true)]
        public bool Test_Move_tile_Num_15(int size, int numMoveTile)
        {
            var newDeck = new LogicDeck(size);
            newDeck.Move(numMoveTile);
            var tileMovable = newDeck.Tiles.First(t => t.Num == numMoveTile);
            var tileEmpty = newDeck.Tiles.First(t => t.Num == 0);

            return tileMovable.Pos == 16 &&
                   tileEmpty.Pos == 15;
        }
       

        private bool EqualsDeck(LogicDeck expected, LogicDeck actual)
        {
            return (expected.Size == actual.Size &&
                    expected.Score == actual.Score &&
                    expected.Victory == actual.Victory &&
                    expected.Tiles.SequenceEqual(actual.Tiles, new ComparerTiles()));

        }
    }

    public class ComparerTiles : IEqualityComparer<LogicTile>
    {
        public bool Equals(LogicTile expected, LogicTile actual)
        {
            return (expected.Num == actual.Num &&
                    expected.Pos == actual.Pos);
        }

        public int GetHashCode(LogicTile obj)
        {
            throw new NotImplementedException();
        }
    }
}
