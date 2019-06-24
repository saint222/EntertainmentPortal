using AutoMapper;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class CreateNewGameHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
        }

        [Test]
        public void TestInitCellsForMap()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Init_cells")
                .Options;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewGameHandler(context, _mapper);
                service.InitCellsForMap(new MapDb() { Id = 1, Size = 3 });
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual(9, context.Cells.Count());
            }
        }

        [Test]
        public void TestPutStartingWordToMap()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "Put_starting_word")
                .Options;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewGameHandler(context, _mapper);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                context.Maps.Add(mapDb);
                service.InitCellsForMap(mapDb);
                service.PutStartingWordToMap(mapDb, "dog");
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual('d', context.Cells.FirstOrDefault(c => c.X == 0 & c.Y == 1).Letter);
                Assert.AreEqual('o', context.Cells.FirstOrDefault(c => c.X == 1 & c.Y == 1).Letter);
                Assert.AreEqual('g', context.Cells.FirstOrDefault(c => c.X == 2 & c.Y == 1).Letter);
            }
        }
    }

}
