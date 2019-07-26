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
    public class CheckLetterHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        ILogger<CheckLetterHandler> _logger;
        IValidator<CheckLetterCommand> _validator;
        ControllerData _controllerData;

        [SetUp]
        public void Setup()
        {
            var validatorForCheckLetterHandler = new Mock<IValidator<CheckLetterCommand>>();
            validatorForCheckLetterHandler.Setup(x => x.Validate(It.IsAny<CheckLetterCommand>()).IsValid)
                .Returns(new ValidationResult().IsValid);
            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mockMapper.CreateMapper();
            _logger = new Mock<ILogger<CheckLetterHandler>>().Object;
            _validator = validatorForCheckLetterHandler.Object;
            _controllerData = new ControllerData()
            {
                Id = 1,
                Letter = "A"
            };
        }

        [TearDown]
        public void AfterTests()
        {
            _validator = null;
            _options = null;
            _logger = null;
            _mapper = null;
            _controllerData = null;
        }
        
        [Test]
        public void Test_Handler_Check_Letter_than_letter_exists_in_word()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCheckLetterExists")
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
                var service = new CheckLetterHandler(context, _mapper, _validator, _logger);
                controllerData = service.Handle(new CheckLetterCommand(_controllerData), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual(0, controllerData.Result.Value.UserErrors);
                Assert.AreEqual("_,_,A,_,_", string.Join(',',controllerData.Result.Value.CorrectLetters));
            }
        }
        
        [Test]
        public void Test_Handler_Check_Letter_than_letter_does_not_exist_in_word()
        {
            Task<Result<ControllerData>> controllerData;
            
            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCheckLetterDoesn'tExist")
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
            
            _controllerData.Letter = "B";
            
            using (var context = new GameDbContext(_options))
            {
                var service = new CheckLetterHandler(context, _mapper, _validator, _logger);
                controllerData = service.Handle(new CheckLetterCommand(_controllerData), CancellationToken.None);
            }
            
            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
                Assert.AreEqual(1, controllerData.Result.Value.UserErrors);
                Assert.AreEqual("_,_,_,_,_", string.Join(',',controllerData.Result.Value.CorrectLetters));
                Assert.AreEqual("A,C", string.Join(',',controllerData.Result.Value.Alphabet));
            }
        }

    }
}