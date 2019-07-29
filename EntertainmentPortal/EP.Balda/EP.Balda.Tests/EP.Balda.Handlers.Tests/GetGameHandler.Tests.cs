using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Handlers;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class GetGameHandler_Tests
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
        public async Task GetGame_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetGame_Handle_NormalData")
                .Options;

            Maybe<Game> result;

            var gameDb = new GameDb
            {
                Id = 1
            };

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new GetGameHandler(context, _mapper);
                context.Games.Add(gameDb);
                context.SaveChanges();
                var request = new GetGame { Id = 1 };
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.HasValue);
            }
        }

        [Test]
        public async Task GetGame_Handle_GameDoesntExist()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetGame_Handle_GameDoesntExist")
                .Options;

            Maybe<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new GetGameHandler(context, _mapper);
                var request = new GetGame { Id = 1 };
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result == Maybe<Game>.None);
            }
        }
    }
}
