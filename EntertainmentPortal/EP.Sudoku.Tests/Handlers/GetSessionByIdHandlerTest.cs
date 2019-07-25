using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class GetSessionByIdHandlerTest
    {
        IMapper _mapper;
        ILogger<GetSessionByIdHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<GetSessionByIdHandler>>().Object;

        }

        [Test]
        public async Task Test_GetSessionByIdHandler_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_GetSessionByIdHandler_Handle_NormalData")
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
                var service = new GetSessionByIdHandler(context, _mapper, _logger);
                await context.Players.AddAsync(playerDb);
                await context.Sessions.AddAsync(sessionDb);
                await context.SaveChangesAsync();
                var session =  await service.Handle(new GetSessionById(1), CancellationToken.None);

                Assert.AreEqual(1, await context.Sessions.CountAsync());
                Assert.AreEqual(1, (int) session.Value.Level);
                Assert.AreEqual(3, session.Value.Hint);
                Assert.AreEqual(1, session.Value.PlayerId);
            }
        }

        [Test]
        public async Task Test_GetSessionByIdHandler_Handle_NotFound()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_GetSessionByIdHandler_Handle_NotFound")
                .Options;

            using (var context = new SudokuDbContext(options))
            {
                var service = new GetSessionByIdHandler(context, _mapper, _logger);
                var session = await service.Handle(new GetSessionById(1), CancellationToken.None);
                Assert.True(!session.HasValue);
            }
        }

    }
}