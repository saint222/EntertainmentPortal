using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Handlers;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Profiles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EP.Hangman.Logic.Tests
{
    [TestFixture]
    public class CreateGameHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        ILogger<CreateDatabaseHandler> _logger;
        IValidator<CheckLetterCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForCheckLetterHandler = new Mock<IValidator<CheckLetterCommand>>();
            validatorForCheckLetterHandler.Setup(x => x.Validate(It.IsAny<CheckLetterCommand>()).IsValid);
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<CreateDatabaseHandler>>().Object;
            _validator = validatorForCheckLetterHandler.Object;
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
        public void Test_Handler_Create_New_Game_Created()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewGame")
                .Options;
            
            using (var context = new GameDbContext(_options))
            {
                var service = new CreateNewGameHandler(context, _mapper, _logger);
                controllerData = service.Handle(new CreateNewGameCommand(), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
            }
        }
        
        [Test]
        public void Test_Handler_Create_New_Game_CorrectData()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewGameCorrectData")
                .Options;
            
            using (var context = new GameDbContext(_options))
            {
                var service = new CreateNewGameHandler(context, _mapper, _logger);
                controllerData = service.Handle(new CreateNewGameCommand(), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.AreEqual(0, controllerData.Result.Value.UserErrors);
                Assert.That(controllerData.Result.Value.CorrectLetters, Is.All.Match("_"));
            }
        }
    }
}