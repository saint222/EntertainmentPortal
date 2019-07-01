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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class GetAllPlayersHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
        }

        [Test]
        public async Task GetAllPlayers_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllPlayers_Handle_NormalData")
                .Options;

            var request = new GetAllPlayers();

            Maybe<IEnumerable<Player>> result;

            var playerDb = new PlayerDb()
            {
                UserName = "Login",
                NickName = "NickName",
                Password = "Password"
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.Users.AddAsync(playerDb);
                await context.SaveChangesAsync();
                var service = new GetAllPlayersHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.HasValue);
            }
        }

        [Test]
        public async Task GetAllPlayers_Handle_NotValidData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllPlayers_Handle_NotValidData")
                .Options;

            var request = new GetAllPlayers();

            Maybe<IEnumerable<Player>> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new GetAllPlayersHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.HasValue);
            }
        }
    }
}
