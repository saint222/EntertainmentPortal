using AutoMapper;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class AddLetterToCellHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
        }

        [Test]
        public void TestAddLetterToCellHandler_True()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Letter")
                .Options;

            bool isAllowed;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddLetterToCellHandler(_mapper, context);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                context.Cells.Add(new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'g' });
                var cell = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 1, Letter = null };
                context.SaveChanges();
                isAllowed = service.IsAllowedCell(cell).Result;
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(isAllowed);
            }
        }

        [Test]
        public void TestAddLetterToCellHandler_False()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_letter")
                .Options;

            bool isAllowed;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddLetterToCellHandler(_mapper, context);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                context.Cells.Add(new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'g' });
                var cell = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 2, Y = 2, Letter = null };
                context.SaveChanges();
                isAllowed = service.IsAllowedCell(cell).Result;
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(isAllowed);
            }
        }
    }
}
