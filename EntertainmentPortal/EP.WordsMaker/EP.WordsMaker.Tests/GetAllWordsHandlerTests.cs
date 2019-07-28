using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAllWordsHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<GetAllWordsCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<GetAllWordsCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<GetAllWordsCommand>()).IsValid);
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
        public void Test_Handler_Get_Words_When_Session_Exists()
        {
            Task<Result<IEnumerable<Word>>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllWordsExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Words
                    .Add(new WordDb()
                    {
                        Id = "dqq"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new GetAllWordsHandler(_mapper, context);
                controllerData = service.Handle(new GetAllWordsCommand(), CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
            }
        }

        [Test]
        public void Test_Handler_Get_Words_When_Session_Does_Not_Exists()
        {
            Task<Result<IEnumerable<Word>>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllWordsDNExists")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                var service = new GetAllWordsHandler(_mapper, context);
                controllerData = service.Handle(new GetAllWordsCommand(), CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("Words array is empty(handler)", controllerData.Result.Error);
            }
        }
    }
}