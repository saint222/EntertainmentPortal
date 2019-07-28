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
using EP.WordsMaker.Logic.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ValidationResult = FluentValidation.Results.ValidationResult;


namespace EP.WordsMaker.Tests
{
    [TestFixture]
    public class AddNewPlayerHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<AddNewPlayerCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<AddNewPlayerCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<AddNewPlayerCommand>()).IsValid)
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
        public void Test_Handler_Create_New_Player_Created()
        {
            Task<Result<Player>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewPlayer")
                .Options;

            using (var context = new GameDbContext(_options))
            {           
                var service = new AddNewPlayerHandler(context, _mapper, _validator);
                controllerData = service.Handle(new AddNewPlayerCommand() {
                    Email = "dada",
                    Id = "aaas",
                    Name = "dasdad"
                }, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
            }
        }
    }
}