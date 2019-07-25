using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class CreateSessionHandlerTest
    {
        IMapper _mapper;
        ILogger<CreateSessionHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<CreateSessionHandler>>().Object;

        }

        [Test]
        public async Task Test_CreateNewSession_RemoveSessionIfExists()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_CreateNewSession_RemoveSessionIfExists")
                .Options;

            var sessionDb = new SessionDb()
            {
                Id = 1,
                Level = 1,
                Hint = 3,
                IsOver = false,
                PlayerDbId = 1
            };

            var userId = Guid.NewGuid().ToString();

            var playerDb = new PlayerDb()
            {
                Id = 1,
                NickName = "Name",
                BestResult = 50,
                WonGames = 2,
                Level = 1,
                IconDb = new AvatarIconDb()
                {
                    Id = 1,
                    Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                    IsBaseIcon = true
                },
                UserId = userId
            };


            using (var context = new SudokuDbContext(options))
            {
                var service = new CreateSessionHandler(context, _mapper, _logger);
                await context.Players.AddAsync(playerDb);
                await context.Sessions.AddAsync(sessionDb);
                await context.SaveChangesAsync();
                service.RemoveSessionIfExists(1, CancellationToken.None);

                Assert.AreEqual(0, await context.Sessions.CountAsync());
            }
        }

        [Test]
        public async Task Test_CreateNewSession_Handle_PlayerDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_CreateNewSession_Handle_PlayerDoesNotExist")
                .Options;

            var userId = Guid.NewGuid().ToString();

            var request = new CreateSessionCommand()
            {
                Level = DifficultyLevel.Easy,
                Hint = 3,
                IsOver = false,
                UserId = userId
            };

            Result<Session> result;

            using (var context = new SudokuDbContext(options))
            {
                var service = new CreateSessionHandler(context, _mapper, _logger);
                result = await service.Handle(request, CancellationToken.None);

                Assert.IsTrue(result.IsFailure);
            }
        }

        [Test]
        public async Task Test_CreateNewSession_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_CreateNewSession_Handle_NormalData")
                .Options;

            var userId = Guid.NewGuid().ToString();

            var request = new CreateSessionCommand()
            {
                Level = DifficultyLevel.Normal,
                Hint = 3,
                IsOver = false,
                UserId = userId
            };

            var playerDb = new PlayerDb()
            {
                Id = 1,
                NickName = "Name",
                BestResult = 50,
                WonGames = 2,
                Level = 1,
                IconDb = new AvatarIconDb()
                {
                    Id = 1,
                    Uri = "http://icons.iconarchive.com/icons/chanut/role-playing/64/Villager-icon.png",
                    IsBaseIcon = true
                },
                UserId = userId
            };

            using (var context = new SudokuDbContext(options))
            {
                var service = new CreateSessionHandler(context, _mapper, _logger);
                await context.Players.AddAsync(playerDb);
                await context.SaveChangesAsync();
                var result = await service.Handle(request, CancellationToken.None);

                Assert.AreEqual(1, await context.Sessions.CountAsync());
                Assert.AreEqual(81, await context.Cells.CountAsync());
                Assert.IsTrue(result.IsSuccess);
            }
        }

    }
}