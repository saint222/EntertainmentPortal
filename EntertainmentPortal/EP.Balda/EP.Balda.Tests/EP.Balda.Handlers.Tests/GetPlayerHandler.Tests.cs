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
    public class GetPlayerHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Player>(It.IsAny<PlayerDb>())).Returns(new Player());
            _mapper = mockMapper.Object;
        }

        [Test]
        public async Task GetPlayerHandler_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlayerHandler_Handle_NormalData")
                .Options;

            var request = new GetPlayer()
            {
                Id = "1"
            };

            Maybe<Player> result;

            var playerDb = new PlayerDb()
            {
                Id = "1",
                UserName = "Login",
                NickName = "NickName",
                Password = "Password"
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.Users.AddAsync(playerDb);
                await context.SaveChangesAsync();
                var service = new GetPlayerHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.HasValue);
            }
        }

        [Test]
        public async Task GetPlayerHandler_Handle_NotValidData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlayerHandler_Handle_NotValidData")
                .Options;

            var request = new GetPlayer()
            {
                Id = "1"
            };

            Maybe<Player> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new GetPlayerHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.HasValue);
            }
        }
    }
}
