using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Handlers;
using EP.Balda.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                .UseInMemoryDatabase(databaseName: "TestInitCellsForMap")
                .Options;

            List<CellDb> cells;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewGameHandler(context, _mapper);
                cells = service.CreateCellsForMap(new MapDb() { Id = 1, Size = 3 });
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual(9, cells.Count());
            }
        }

        [Test]
        public async Task TestPutStartingWordToMap()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestPutStartingWordToMap")
                .Options;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewGameHandler(context, _mapper);
                var mapDb = new MapDb() { Id = 1, Size = 3 };
                context.Maps.Add(mapDb);
                var cells = service.CreateCellsForMap(mapDb);
                mapDb.Cells = cells;
                await service.PutStartingWordToMap(mapDb, "dog");
                context.SaveChanges();
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual('d', context.Cells.FirstOrDefault(c => c.X == 0 & c.Y == 1).Letter);
                Assert.AreEqual('o', context.Cells.FirstOrDefault(c => c.X == 1 & c.Y == 1).Letter);
                Assert.AreEqual('g', context.Cells.FirstOrDefault(c => c.X == 2 & c.Y == 1).Letter);
            }
        }

        [Test]
        public async Task TestCreateNewGame_Handle_PlayerDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewGame_Handle_PlayerDoesNotExist")
                .Options;

            var request = new CreateNewGameCommand()
            {
                MapSize = 3,
                PlayerId = "1"
            };

            Result<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewGameHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.IsFailure);
            }
        }

        [Test]
        public async Task TestCreateNewGame_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewGame_Handle_NormalData")
                .Options;

            var request = new CreateNewGameCommand()
            {
                PlayerId = "1",
                MapSize = 3
            };

            var playerDb = new PlayerDb()
            {
                Id = "1",
                Created = DateTime.UtcNow,
                UserName = "Login",
                NickName = "Name",
                Password = "1234567",
            };

            var wordRuDb = new WordRuDb()
            {
                Id = 1,
                Word = "dog"
            };

            Result<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewGameHandler(context, _mapper);
                await context.WordsRu.AddAsync(wordRuDb);
                await context.Users.AddAsync(playerDb);
                await context.SaveChangesAsync();
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual(1, await context.Games.CountAsync());
                Assert.IsTrue(result.IsSuccess);
            }
        }
    }

}
