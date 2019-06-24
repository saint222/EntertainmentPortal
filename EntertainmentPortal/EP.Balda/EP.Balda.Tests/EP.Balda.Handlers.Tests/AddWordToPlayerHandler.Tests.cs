using AutoMapper;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class AddWordToPlayerHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
        }

        [Test]
        public void TestIsWordCorrect_True()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Letter")
                .Options;

            bool isCorrect;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(_mapper, context);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                var cell1 = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new CellDb() { Id = 2, MapId = 1, Map = mapDb, X = 0, Y = 1, Letter = 'o' };
                var cell3 = new CellDb() { Id = 3, MapId = 1, Map = mapDb, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<CellDb>() { cell1, cell2, cell3 };

                context.WordsRu.Add(new WordRuDb() { Id = 1, Word = "dog" });
                context.SaveChanges();
                isCorrect = service.IsWordCorrect(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(isCorrect);
            }
        }

        [Test]
        public void TestIsWordCorrect_False()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_letter")
                .Options;

            bool isCorrect;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(_mapper, context);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                var cell1 = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new CellDb() { Id = 2, MapId = 1, Map = mapDb, X = 0, Y = 2, Letter = 'o' };
                var cell3 = new CellDb() { Id = 3, MapId = 1, Map = mapDb, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<CellDb>() { cell1, cell2, cell3 };

                context.WordsRu.Add(new WordRuDb() { Id = 1, Word = "dog" });
                context.SaveChanges();
                isCorrect = service.IsWordCorrect(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(isCorrect);
            }
        }

        [Test]
        public void TestGetSelectedWord()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_letter")
                .Options;

            string word;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(_mapper, context);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                var cell1 = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new CellDb() { Id = 2, MapId = 1, Map = mapDb, X = 0, Y = 1, Letter = 'o' };
                var cell3 = new CellDb() { Id = 3, MapId = 1, Map = mapDb, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<CellDb>() { cell1, cell2, cell3 };

                context.SaveChanges();
                word = service.GetSelectedWord(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual("dog", word);
            }
        }
    }
}
