using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace EP.WordsMaker.Tests
{
    public class AddNewGameHandlerTests
    {
        IMapper _mapper;
        DbContextOptions<GameDbContext> _options;
        IValidator<AddNewGameCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var validatorForAddPlayerHandler = new Mock<IValidator<AddNewGameCommand>>();
            validatorForAddPlayerHandler.Setup(x => x.Validate(It.IsAny<AddNewGameCommand>()).IsValid)
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
        public void Test_Handler_Create_New_Game_Created()
        {
            Task<Result<Game>> controllerData;

            _options = new DbContextOptionsBuilder<GameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewGame")
                .Options;

            using (var context = new GameDbContext(_options))
            {
                context.Players
                    .Add(new PlayerDb()
                    {
                        Id = "asfadfsc",
                        BestScore = 1123,
                        BestScoreId = 12,
                        Email = "fsaas",
                        LastGame = DateTime.Now,
                        Name = "adfadas",
                        Score = 123
                    });
                context.SaveChanges();
            }

            using (var context = new GameDbContext(_options))
            {
                var service = new AddNewGameHandler(context, _mapper, _validator);

                controllerData = service.Handle(new AddNewGameCommand() {PlayerId = "asfadfsc"}, CancellationToken.None);
            }

            using (var context = new GameDbContext(_options))
            {
                Assert.IsTrue(controllerData.Result.IsSuccess);
            }
        }
    }
}