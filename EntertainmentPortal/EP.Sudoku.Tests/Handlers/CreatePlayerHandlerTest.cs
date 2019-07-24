using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Profiles;
using FluentValidation;
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
    public class CreatePlayerHandlerTest
    {
        IMapper _mapper;        
        IValidator<CreatePlayerCommand> _validator;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();            
            var validMock = new Mock<IValidator<CreatePlayerCommand>>();
            validMock.Setup(x => x.Validate(It.IsAny<CreatePlayerCommand>()).IsValid);
            _validator = validMock.Object;
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

            Result<Player> result;

            using (var context = new SudokuDbContext(options))
            {
                var service = new CreatePlayerHandler(context, _mapper, _validator);                
                await context.SaveChangesAsync();
                result = await service.Handle(playerDb, CancellationToken.None);
            }

            using (var context = new SudokuDbContext(options))
            {
                Assert.AreEqual(1, await context.Players.CountAsync());                
                Assert.IsTrue(result.IsSuccess);
            }
        }
    }
}
