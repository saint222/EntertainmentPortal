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
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EP.WordsMaker.Tests
{
    public class DeletePlayerHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<DeletePlayerCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<DeletePlayerCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<DeletePlayerCommand>()).IsValid)
                .Returns(new ValidationResult().IsValid);
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new PlayerProfile()));
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
        public void Test_Handler_Delete_Player_When_Exist()
        {
            Task<Result<Player>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeletePlayerWhenExist")
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
                var service = new DeletePlayerHandler(context, _mapper, _validator);
                controllerData = service.Handle(new DeletePlayerCommand()
                {
                    Id = "dqq"
                }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual(null, controllerData.Result.Value);
            }
        }

        [Test]
        public void Test_Handler_Delete_Player_When_Does_Not_Exist()
        {
            Task<Result<Player>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeletePlayerWhenDNExist")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                var service = new DeletePlayerHandler(context, _mapper, _validator);
                controllerData = service.Handle(new DeletePlayerCommand()
                {
                    Id = "dqq"
                }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("Data wasn't found", controllerData.Result.Error);
            }
        }
    }
}