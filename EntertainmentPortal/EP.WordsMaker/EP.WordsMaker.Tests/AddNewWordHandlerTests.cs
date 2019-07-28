using System;
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
    public class AddNewWordHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<AddNewWordCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<AddNewWordCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<AddNewWordCommand>()).IsValid)
                .Returns(new ValidationResult().IsValid);
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
        public void Test_Handler_Create_New_Word()
        {
            Task<Result<Word>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateWord")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = "qewr",
                        PlayerId = "13132",
                        KeyWord = "КАЗИНО"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new AddNewWordHandler(context, _mapper, _validator);

                controllerData = service.Handle(new AddNewWordCommand() {GameId = "qewr", Value = "КОЗА"}, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
            }
        }

        [Test]
        public void Test_Handler_Create_Word_When_Word_Does_Not_Exists()
        {
            Task<Result<Word>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateGameDNExist")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = "qewr",
                        PlayerId = "13132",
                        KeyWord = "КАЗИНО"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new AddNewWordHandler(context, _mapper, _validator);

                controllerData = service.Handle(new AddNewWordCommand() { GameId = "qewr", Value = "КОЗО" }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("This word incorrect (Word Handler)", controllerData.Result.Error);
            }
        }

        [Test]
        public void Test_Handler_Create_Word_Created_When_Word_Repeat()
        {
            Task<Result<Word>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateWordWhenWordRepeat")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = "qewr",
                        PlayerId = "13132",
                        KeyWord = "КАЗИНО"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                context.Words
                    .Add(new WordDb()
                    {
                        GameId = "qewr",
                        Value = "КОЗА"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new AddNewWordHandler(context, _mapper, _validator);

                controllerData = service.Handle(new AddNewWordCommand() { GameId = "qewr", Value = "КОЗА" }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("This word already exists(Word Handler)", controllerData.Result.Error);
            }
        }
    }
}