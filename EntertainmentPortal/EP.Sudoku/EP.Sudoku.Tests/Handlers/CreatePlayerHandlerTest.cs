using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class CreatePlayerHandlerTest
    {
        IMapper _mapper;        
        IValidator<CreatePlayerCommand> _validator;
        ILogger<PlayerValidator> _logger;        

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<PlayerValidator>>().Object;           
        }

        [Test]
        public async Task TestCreateNewPlayer_Handle_CorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewPlayer_Handle_CorrectData")
                .Options;

            var userId = Guid.NewGuid().ToString();

            var playerDb = new CreatePlayerCommand()
            {
                NickName = "Bob",
                IconId = 1,                
                UserId = userId
            };

            Result<Player> player;

            using (var context = new SudokuDbContext(options))
            {
                _validator = new PlayerValidator(context, _logger);
                var service = new CreatePlayerHandler(context, _mapper, _validator);
                player = await service.Handle(playerDb, CancellationToken.None);

                Assert.AreEqual(1, await context.Players.CountAsync());                
                Assert.IsTrue(player.IsSuccess);
            }
        }

        [Test]
        public async Task TestCreateNewPlayer_Handle_IncorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewPlayer_Handle_IncorrectData")
                .Options;
            using (var context = new SudokuDbContext(options))
            {
                var userId = Guid.NewGuid().ToString();
                context.Players
                    .Add(new PlayerDb()
                    {
                        Id = 1,
                        NickName = "Bob",
                        BestResult = 10,
                        WonGames = 2,
                        Level = 1,
                        IconDb = new AvatarIconDb()
                        {
                            Id = 1,
                            Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                            IsBaseIcon = true
                        },
                        UserId = userId
                    });
                context.SaveChanges();
            }            

            using (var context = new SudokuDbContext(options))
            {
                var userId = Guid.NewGuid().ToString();

                var playerDb = new CreatePlayerCommand() 
                {
                    NickName = "Bob", //the same name
                    IconId = 1,
                    UserId = userId
                };
                Result<Player> player;

                _validator = new PlayerValidator(context, _logger);
                var service = new CreatePlayerHandler(context, _mapper, _validator);
                player = await service.Handle(playerDb, CancellationToken.None);
                
                Assert.IsTrue(player.IsFailure);
                Assert.AreEqual(player.Error, "A player with the same nickname already exists, change the nickname!");
            }
        }
    }
}
