using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Handlers;
using EP.WordsMaker.Logic.Models;
using EP.WordsMaker.Logic.Profiles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EP.WordsMaker.Tests
{
    public class GetPlayerHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<GetPlayerCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<GetPlayerCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<GetPlayerCommand>()).IsValid);
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new GameProfile()));
            _mapper = mockMapper.CreateMapper();
            _validator = validatorForAddPlayerHandler.Object;
        }

        [TearDown]
        public void AfterTests()
        {
            _validator = null;
            _options = null;
            _mapper = null;
        }

        [Test]
        public void Test_Handler_Get_Player_When_Session_Exists()
        {
            Task<Result<Player>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetPlayer")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Players
                    .Add(new PlayerDb()
                    {
                        Id = "dqq",
                        Email = "31323"
                        
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new GetPlayerHandler(_mapper, context);
                controllerData = service.Handle(new GetPlayerCommand() {Id = "dqq"}, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual("dqq", controllerData.Result.Value.Id);
            }
        }

        [Test]
        public void Test_Handler_Get_Player_When_Player_Does_Not_Exists()
        {
            Task<Result<Player>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetPlayerWhenDNExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                var service = new GetPlayerHandler(_mapper, context);
                controllerData = service.Handle(new GetPlayerCommand() {Id = "da" }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("Player not found(handler)", controllerData.Result.Error);
            }
        }
    }
}