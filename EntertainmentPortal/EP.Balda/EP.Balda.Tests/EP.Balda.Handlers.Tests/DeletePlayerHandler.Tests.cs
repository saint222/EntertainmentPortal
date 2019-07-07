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
    public class DeletePlayerHandler_Tests
    {
        IMapper _mapper;
        
        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
        }

        [Test]
        public async Task DeletePlayer_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "DeletePlayer_Handle_NormalData")
                .Options;

            var request = new DeletePlayerCommand()
            {
                Id = 1
            };

            Result<Player> result;

            var playerDb = new PlayerDb()
            {
                Id = 1,
                Login = "Login",
                NickName = "NickName",
                Password = "Password"
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.Players.AddAsync(playerDb);
                await context.SaveChangesAsync();
                var service = new DeletePlayerHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.IsSuccess);
            }
        }

        [Test]
        public async Task DeletePlayer_Handle_NotValidData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "DeletePlayer_Handle_NotValidData")
                .Options;

            var request = new DeletePlayerCommand()
            {
                Id = 1
            };

            Result<Player> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new DeletePlayerHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.IsSuccess);
            }
        }
    }
}
