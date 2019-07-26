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
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EP.Hangman.Logic.Tests
{
    [TestFixture]
    public class DeleteGameSessionHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        ILogger<DeleteGameSessionCommandHandler> _logger;
        IValidator<DeleteGameSessionCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForDeleteGameSessionHandler = new Mock<IValidator<DeleteGameSessionCommand>>();
            validatorForDeleteGameSessionHandler.Setup(x => x.Validate(It.IsAny<DeleteGameSessionCommand>()).IsValid)
                .Returns(new ValidationResult().IsValid);
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<DeleteGameSessionCommandHandler>>().Object;
            _validator = validatorForDeleteGameSessionHandler.Object;
        }

        [TearDown]
        public void AfterTests()
        {
            _validator = null;
            _options = null;
            _logger = null;
            _mapper = null;
        }
        
        [Test]
        public void Test_Handler_Delete_Game_Session_From_Data_Base_When_Session_Exists()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteGameSessionExists")
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
                var service = new DeleteGameSessionCommandHandler(context, _mapper, _validator, _logger);
                controllerData = service.Handle(new DeleteGameSessionCommand("1"), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual(null, controllerData.Result.Value);

            }
        }
        
        [Test]
        public void Test_Handler_Delete_Game_Session_From_Data_Base_When_Session_Does_Not_Exist()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeleteGameSessionDoesn'tExist")
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
                var service = new DeleteGameSessionCommandHandler(context, _mapper, _validator, _logger);
                controllerData = service.Handle(new DeleteGameSessionCommand("2"), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsFailure);
                Assert.AreEqual("Data wasn't found", controllerData.Result.Error);
            }
        }

    }
}