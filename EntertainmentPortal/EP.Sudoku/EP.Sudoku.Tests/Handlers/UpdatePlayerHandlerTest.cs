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
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class UpdatePlayerHandlerTest
    {
        IMapper _mapper;
        IValidator<UpdatePlayerCommand> _validator;        
        ILogger<PlayerValidator> logger;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();            
            logger = new Mock<ILogger<PlayerValidator>>().Object;            
        }

        [Test]
        public async Task TestUpdatePlayer_Handle_CorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdatePlayer_Handle_CorrectData")
                .Options;
            using (var context = new SudokuDbContext(options))
            {                
                context.Players
                    .Add(new PlayerDb()
                    {
                        Id = 1,
                        NickName = "Bob",                                          
                        UserId = "1",
                        IconDb = new AvatarIconDb()
                        {
                            Id = 1,
                            Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                            IsBaseIcon = true
                        },
                    });
                context.SaveChanges();
            }

            using (var context = new SudokuDbContext(options))
            {
                var p = new Player()
                {
                    Id = 1,
                    NickName = "Sam",                                      
                    UserId = "1",
                    Icon = new AvatarIcon()
                    {
                        Id = 1,
                        Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                        IsBaseIcon = true
                    },
                };
                var playerDb = new UpdatePlayerCommand(p);               
                
                Result<Player> player;

                _validator = new EditPlayerValidator(context, logger);
                var service = new UpdatePlayerHandler(context, _mapper, _validator);
                player = await service.Handle(playerDb, CancellationToken.None);

                Assert.IsTrue(player.IsSuccess);                
            }
        }

        [Test]
        public async Task TestUpdatePlayer_Handle_AlreadyExists()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestUpdatePlayer_Handle_AlreadyExists")
                .Options;
            using (var context = new SudokuDbContext(options))
            {
                context.Players
                    .Add(new PlayerDb()
                    {
                        Id = 1,
                        NickName = "Bob",
                        UserId = "1",
                        IconDb = new AvatarIconDb()
                        {
                            Id = 1,
                            Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                            IsBaseIcon = true
                        },
                    });
                context.SaveChanges();
            }

            using (var context = new SudokuDbContext(options))
            {
                var p = new Player()
                {
                    Id = 1,
                    NickName = "Bob",
                    UserId = "1",
                    Icon = new AvatarIcon()
                    {
                        Id = 1,
                        Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                        IsBaseIcon = true
                    },
                };
                var playerDb = new UpdatePlayerCommand(p);

                Result<Player> player;

                _validator = new EditPlayerValidator(context, logger);
                var service = new UpdatePlayerHandler(context, _mapper, _validator);
                player = await service.Handle(playerDb, CancellationToken.None);

                Assert.AreEqual(player.Error, "A player with the same nickname already exists, change the nickname!");
            }
        }
    }
}

