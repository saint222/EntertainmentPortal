using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NewDeckHandlerTests
    {
        private DeckDbContext _context;
        private IMapper _mapper;
        [SetUp]
        public void MockInitialize()
        {
            var mockContext = new Mock<DeckDbContext>(new DbContextOptions<DeckDbContext>());
            _context = mockContext.Object;
            var mockMapper = new Mock<IMapper>();
            _mapper = mockMapper.Object;
        }

        [Test]
        public void Test_Create_New_Deck()
        {
            var newDeck = new DeckDb(4);

            var expectedDeck = new DeckDb()
            {
                Score = 0,
                Victory = false,
                Tiles = new List<TileDb>()
                {
                    new TileDb(1),
                    new TileDb(2),
                    new TileDb(3),
                    new TileDb(4),
                    new TileDb(5),
                    new TileDb(6),
                    new TileDb(7),
                    new TileDb(8),
                    new TileDb(9),
                    new TileDb(10),
                    new TileDb(11),
                    new TileDb(12),
                    new TileDb(13),
                    new TileDb(14),
                    new TileDb(15),
                },
                EmptyTile = new TileDb(16)
            };

            Assert.IsTrue(Equals(expectedDeck, newDeck));

        }

        [Test]
        public void Test_Unsort_Deck()
        {
            var newDeck = new DeckDb(4);
            var handle = new NewDeckHandler(_context,_mapper);
            handle.Unsort(newDeck);
            var expectdDeck = new DeckDb()
            {
                Score = 0,
                Victory = false,
                Tiles = new List<TileDb>()
                {
                    new TileDb(1),
                    new TileDb(2),
                    new TileDb(3),
                    new TileDb(4),
                    new TileDb(5),
                    new TileDb(6),
                    new TileDb(7),
                    new TileDb(8),
                    new TileDb(9),
                    new TileDb(10),
                    new TileDb(11),
                    new TileDb(12),
                    new TileDb(13),
                    new TileDb(14),
                    new TileDb(15),
                },
                EmptyTile = new TileDb(16)
            };

            Assert.IsFalse(Equals(expectdDeck, newDeck));
        }
        
        private bool Equals(DeckDb expected, DeckDb actual)
        {
            return (expected.Score == actual.Score &&
                    expected.Victory == actual.Victory &&
                    expected.Tiles.SequenceEqual(actual.Tiles ,new ComparerTiles()));

        }
    }

    public class ComparerTiles : IEqualityComparer< TileDb >
    {
        public bool Equals(TileDb expected, TileDb actual)
        {
            return (expected.Id == actual.Id &&
                    expected.Num == actual.Num &&
                    expected.Pos == actual.Pos);
        }

        public int GetHashCode(TileDb obj)
        {
            throw new NotImplementedException();
        }
    }
}
