using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Handlers;
using EP.WordsMaker.Logic.Profiles;
using EP.WordsMaker.Logic.Queries;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EP.WordsMaker.Tests
{
    public class GetAllGamesHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<GetAllGames> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<GetAllGames>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<GetAllGames>()).IsValid);
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
        public void Test_Handler_Get_Games_When_Session_Exists()
        {
            Task<Maybe<IEnumerable<GameDb>>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllGamesExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = "1111"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new GetAllGamesHandler(_mapper, context);
                controllerData = service.Handle(new GetAllGames(), CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.HasValue);
            }
        }

        [Test]
        public void Test_Handler_Get_Games_When_Session_Does_Not_Exists()
        {
            Task<Maybe<IEnumerable<PlayerDb>>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllGamesDNExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                var service = new GetAllPlayersHandler(_mapper, context);
                controllerData = service.Handle(new GetAllPlayers(), CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.HasNoValue);
            }
        }
    }
}