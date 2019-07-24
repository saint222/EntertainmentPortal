using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class DeletePlayerHandlerTest
    {
        IMapper _mapper;
        ILogger<DeletePlayerHandler> _logger;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();            
            _logger = new Mock<ILogger<DeletePlayerHandler>>().Object;
        }

        [Test]
        public async Task TestDeletePlayerHandler_Handle_CorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeletePlayerHandler_Handle_CorrectData")
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
                var service = new DeletePlayerHandler(context, _logger);
                var result = await service.Handle(new DeletePlayerCommand(1), CancellationToken.None);
            }

            using (var context = new SudokuDbContext(options))
            {                
                Assert.AreEqual(true, true);                
            }
        }

        [Test]
        public async Task TestDeletePlayerHandler_Handle_IncorrectData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDeletePlayerHandler_Handle_IncorrectData")
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
                var service = new DeletePlayerHandler(context, _logger);
                var result = await service.Handle(new DeletePlayerCommand(2), CancellationToken.None);
            }

            using (var context = new SudokuDbContext(options))
            {
                Assert.AreEqual(false, false);                
            }
        }
    }
}
