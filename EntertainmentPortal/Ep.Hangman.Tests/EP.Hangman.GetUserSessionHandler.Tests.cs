using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Context;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Handlers;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Profiles;
using EP.Hangman.Logic.Queries;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EP.Hangman.Logic.Tests
{
    [TestFixture]
    public class GetUserSessionHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        ILogger<GetUserSessionHandler> _logger;
        IValidator<GetUserSession> _validator;
        private IMemoryCache _cash;

        [SetUp]
        public void Setup()
        {
            var validatorForDeleteGameSessionHandler = new Mock<IValidator<GetUserSession>>();
            validatorForDeleteGameSessionHandler.Setup(x => x.Validate(It.IsAny<GetUserSession>()).IsValid)
                .Returns(new ValidationResult().IsValid);
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<GetUserSessionHandler>>().Object;
            _validator = validatorForDeleteGameSessionHandler.Object;
            var entryMock = new Mock<ICacheEntry>();
            var cash =  new Mock<IMemoryCache>();
            cash.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(entryMock.Object);
            _cash = cash.Object;
        }

        [TearDown]
        public void AfterTests()
        {
            _validator = null;
            _options = null;
            _logger = null;
            _mapper = null;
            _cash = null;
        }
        
        [Test]
        public void Test_Handler_Get_Game_Session_From_Data_Base_When_Session_Exists()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetGameSessionExists")
                .Options;
            
            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = 1,
                        Alphabet = "A,B,C",
                        PickedWord = "START",
                        UserErrors = 0,
                        CorrectLetters = "_,_,_,_,_"
                    });
                context.SaveChanges();
            }
            using (var context = new GameDbContext(_options))
            {
                var service = new GetUserSessionHandler(context, _mapper, _validator, _logger, _cash);
                controllerData = service.Handle(new GetUserSession("1"), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual(1, controllerData.Id);

            }
        }
        
        [Test]
        public void Test_Handler_Get_Game_Session_From_Data_Base_When_Session_Does_Not_Exist()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetGameSessionDoesn'tExist")
                .Options;
            
            using (var context = new GameDbContext(_options))
            {
                context.Games
                    .Add(new GameDb()
                    {
                        Id = 1,
                        Alphabet = "A,B,C",
                        PickedWord = "START",
                        UserErrors = 0,
                        CorrectLetters = "_,_,_,_,_"
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new GetUserSessionHandler(context, _mapper, _validator, _logger, _cash);
                controllerData = service.Handle(new GetUserSession("2"), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("Session not found", controllerData.Result.Error);
            }
        }

    }
}