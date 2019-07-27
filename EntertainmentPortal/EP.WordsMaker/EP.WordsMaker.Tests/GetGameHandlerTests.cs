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
    public class GetGameHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<GetGameCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<GetGameCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<GetGameCommand>()).IsValid);
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
        public void Test_Handler_Get_Game_When_Session_Exists()
        {
            Task<Result<Game>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteGameSessionExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = "dqq",
                        PlayerId = "dada"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new GetGameHandler(_mapper, context);
                controllerData = service.Handle(new GetGameCommand() {PlayerId = "dada"}, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual("dqq", controllerData.Result.Value.Id);
            }
        }

        [Test]
        public void Test_Handler_Get_Game_When_Session_Does_Not_Exists()
        {
            Task<Result<Game>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteGameSessionExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = "dqq",
                        PlayerId = "dada"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new GetGameHandler(_mapper, context);
                controllerData = service.Handle(new GetGameCommand() { PlayerId = "da" }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("Game not found (Game handler)", controllerData.Result.Error);
            }
        }
    }
}