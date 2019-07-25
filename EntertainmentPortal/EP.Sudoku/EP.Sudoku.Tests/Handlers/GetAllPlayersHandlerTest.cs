using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class GetAllPlayersHandlerTest
    {
        IMapper _mapper;
        IMemoryCache _memoryCache;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();
            var entryMock = new Mock<ICacheEntry>();
            var memoryCache = new Mock<IMemoryCache>();
            memoryCache.Setup(repo => repo.CreateEntry(It.IsAny<object>())).Returns(entryMock.Object);
            _memoryCache = memoryCache.Object;
        }

        [Test]
        public async Task TestGetAllPlayersHandler_Handle_CorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllPlayersHandler_Handle_CorrectData")
                .Options;
            using (var context = new SudokuDbContext(options))
            {
                var userId = Guid.NewGuid().ToString();
                context.Players
                    .AddRange(new PlayerDb()
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
                    },
                    new PlayerDb()
                    {
                        Id = 2,
                        NickName = "Sam",
                        BestResult = 20,
                        WonGames = 3,
                        Level = 2,
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
            IEnumerable<Player> players;
            using (var context = new SudokuDbContext(options))
            {
                var service = new GetAllPlayersHandler(context, _mapper, _memoryCache);
                players = await service.Handle(new GetAllPlayers(), CancellationToken.None);
            }

            using (var context = new SudokuDbContext(options))
            {
                Assert.AreEqual(players, players);
                Assert.AreNotEqual(null, players);
            }
        }

        [Test]
        public async Task TestGetAllPlayersHandler_Handle_NotFound()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetAllPlayersHandler_Handle_NotFound")
                .Options;

            IEnumerable<Player> players;

            // there are no players in the database
            using (var context = new SudokuDbContext(options))
            {
                var service = new GetAllPlayersHandler(context, _mapper, _memoryCache);
                players = await service.Handle(new GetAllPlayers(), CancellationToken.None);
                Assert.AreEqual(0, players.Count()); // if players.count == 0, the result of the test is "ok"
            }            
        }
    }
}
