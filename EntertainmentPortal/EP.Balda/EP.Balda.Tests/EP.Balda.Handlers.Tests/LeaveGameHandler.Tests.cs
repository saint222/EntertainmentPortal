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
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class LeaveGameHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Game>(It.IsAny<GameDb>())).Returns(new Game());
            _mapper = mockMapper.Object;
        }

        [Test]
        public async Task LeaveGame_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "LeaveGame_Handle_NormalData")
                .Options;

            Result<Game> result;

            var request = new LeaveGameCommand
            {
                GameId = 1
            };

            var gameDb = new GameDb
            {
                Id = 1
            };

            using (var context = new BaldaGameDbContext(options))
            {
                context.Games.Add(gameDb);
                context.SaveChanges();
                var service = new LeaveGameHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.IsSuccess);
            }
        }

        [Test]
        public async Task LeaveGame_Handle_GameDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "LeaveGame_Handle_GameDoesNotExist")
                .Options;

            Result<Game> result;

            var request = new LeaveGameCommand
            {
                GameId = 1
            };

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new LeaveGameHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.IsSuccess);
            }
        }
    }
}
