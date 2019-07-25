using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Logic.Models;
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
    public class GetPlayerByIdHandlerTest
    {
        IMapper _mapper;
        ILogger<GetPlayerByIdHandler> _logger;        

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();            
            _logger = new Mock<ILogger<GetPlayerByIdHandler>>().Object;            
        }

        [Test]
        public async Task TestGetPlayerByIdHandler_Handle_CorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetPlayerByIdHandler_Handle_CorrectData")
                .Options;

            var userId = Guid.NewGuid().ToString();

            var playerDb = new PlayerDb()
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
            };            

            using (var context = new SudokuDbContext(options))
            {
                var service = new GetPlayerByIdHandler(context, _mapper, _logger); //here the argumens' order is fundamental                
                await context.Players.AddAsync(playerDb);                
                await context.SaveChangesAsync();
                var player = await service.Handle(new GetPlayerById(1), CancellationToken.None);
                Assert.AreEqual(1, await context.Players.CountAsync());
                Assert.AreEqual("Bob", player.NickName);
                Assert.AreEqual(10, player.BestResult);
                Assert.AreEqual(2, player.WonGames);
                Assert.AreEqual(1, player.Level);
                Assert.AreEqual(1, player.Icon.Id);                
            }            
        }

        [Test]
        public async Task TestGetPlayerByIdHandler_Handle_IncorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetPlayerByIdHandler_Handle_IncorrectData")
                .Options;

            var userId = Guid.NewGuid().ToString();

            var playerDb = new PlayerDb()
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
            };

            using (var context = new SudokuDbContext(options))
            {
                var service = new GetPlayerByIdHandler(context, _mapper, _logger); //here the argumens' order is fundamental                
                await context.Players.AddAsync(playerDb);
                await context.SaveChangesAsync();
                var player = await service.Handle(new GetPlayerById(1), CancellationToken.None);
                Assert.AreNotEqual(100, await context.Players.CountAsync());
                Assert.AreNotEqual("Sam", player.NickName);
                Assert.AreNotEqual(100, player.BestResult);
                Assert.AreNotEqual(200, player.WonGames);
                Assert.AreNotEqual(100, player.Level);
                Assert.AreNotEqual(100, player.Icon.Id);
            }
        }

        [Test]
        public async Task TestGetPlayerByIdHandler_Handle_NotFound()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetPlayerByIdHandler_Handle_NotFound")
                .Options;

            // there is no a player with the Id == 1 in the database
            using (var context = new SudokuDbContext(options))
            {
                var service = new GetPlayerByIdHandler(context, _mapper, _logger);
                var player = await service.Handle(new GetPlayerById(1), CancellationToken.None);
                Assert.IsNull(player); // if the player == null, the result of the test is "ok"
            }
        }
    }
}
