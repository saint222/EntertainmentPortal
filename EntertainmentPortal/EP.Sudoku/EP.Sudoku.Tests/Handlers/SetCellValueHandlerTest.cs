using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Handlers;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EP.Sudoku.Tests.Handlers
{
    [TestFixture]
    public class SetCellValueHandlerTest
    {
        IMapper _mapper;
        IValidator<SetCellValueCommand> _validator;
        ILogger<SetCellValueHandler> _logger;
        List<CellDb> _baseGrid = new List<CellDb>();

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<SetCellValueHandler>>().Object;


            int[,] baseGrid = new int[9, 9] {
                {1, 2, 3, 4, 5, 6, 7, 8, 9},
                {4, 5, 6, 7, 8, 9, 1, 2, 3},
                {7, 8, 9, 1, 2, 3, 4, 5, 6},
                {2, 3, 4, 5, 6, 7, 8, 9, 1},
                {5, 6, 7, 8, 9, 1, 2, 3, 4},
                {8, 9, 1, 2, 3, 4, 5, 6, 7},
                {3, 4, 5, 6, 7, 8, 9, 1, 2},
                {6, 7, 8, 9, 1, 2, 3, 4, 5},
                {9, 1, 2, 3, 4, 5, 6, 7, 8}
            };
            _baseGrid = GridToCells(baseGrid);
        }

        public List<CellDb> GridToCells(int[,] grid)
        {
            List<CellDb> cells = new List<CellDb>();
            long index = 1;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CellDb cell = new CellDb
                    {
                        Id = index,
                        X = i + 1,
                        Y = j + 1,
                        Value = grid[i, j]
                    };
                    cells.Add(cell);
                    index++;
                }
            }

            return cells;
        }

        [Test]
        public async Task Test_SetCellValueHandler_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_SetCellValueHandler_Handle_NormalData")
                .Options;

            var sessionDb = new SessionDb()
            {
                Id = 1,
                Level = 1,
                Hint = 3,
                Score = 0,
                Error = 0,
                IsOver = false,
                PlayerDbId = 1,
                SquaresDb = _baseGrid
            };

            sessionDb.SquaresDb.First(x => x.Id == 1).Value = 0;
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
                _validator = new SetCellValueValidator(context);
                var service = new SetCellValueHandler(context, _mapper, _validator, _logger);
                await context.Players.AddAsync(playerDb);
                await context.Sessions.AddAsync(sessionDb);
                await context.SaveChangesAsync();

                var request = new SetCellValueCommand()
                {
                    Id = 1,
                    Value = 1,
                    SessionId = 1
                };

                var result = await service.Handle(request, CancellationToken.None);

                Assert.IsTrue(result.IsSuccess);
                Assert.AreEqual(1, (int)result.Value.Squares.First(x => x.Id == 1).Value);
            }
        }

        [Test]
        public async Task Test_GetHintHandler_IncorrectValueRowOrColumn()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_GetHintHandler_IncorrectValueRowOrColumn")
                .Options;

            var sessionDb = new SessionDb()
            {
                Id = 1,
                Level = 1,
                Hint = 3,
                Score = 0,
                Error = 0,
                IsOver = false,
                PlayerDbId = 1,
                SquaresDb = _baseGrid
            };
            sessionDb.SquaresDb.First(x => x.Id == 1).Value = 0;

            using (var context = new SudokuDbContext(options))
            {
                _validator = new SetCellValueValidator(context);
                var service = new SetCellValueHandler(context, _mapper, _validator, _logger);
                await context.Sessions.AddAsync(sessionDb);
                await context.SaveChangesAsync();


                var request = new SetCellValueCommand()
                {
                    Id = 1,
                    Value = 2,
                    SessionId = 1
                };

                var result = await service.Handle(request, CancellationToken.None);

                Assert.IsTrue(result.IsFailure);
                Assert.AreEqual(result.Error, "Incorrect value. The row or column already has this number!");
            }
        }

        [Test]
        public async Task Test_SetCellValueHandler_AddScore()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_SetCellValueHandler_AddScore")
                .Options;

            var sessionDb = new SessionDb()
            {
                Id = 1,
                Level = 1,
                Hint = 3,
                Score = 0,
                Error = 0,
                IsOver = false,
                PlayerDbId = 1,
                SquaresDb = _baseGrid
            };

            using (var context = new SudokuDbContext(options))
            {
                _validator = new SetCellValueValidator(context);
                var service = new SetCellValueHandler(context, _mapper, _validator, _logger);
                await context.Sessions.AddAsync(sessionDb);
                await context.SaveChangesAsync();;

                var request = new SetCellValueCommand()
                {
                    Id = 1,
                    Value = 1,
                    SessionId = 1
                };

                await service.AddScore(request, CancellationToken.None);

                Assert.AreEqual(1, context.Sessions.First(x => x.Id == request.SessionId).Score);
            }
        }

        [Test]
        public async Task Test_SetCellValueHandler_AddError()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_SetCellValueHandler_AddError")
                .Options;

            var sessionDb = new SessionDb()
            {
                Id = 1,
                Level = 1,
                Hint = 3,
                Score = 0,
                Error = 0,
                IsOver = false,
                PlayerDbId = 1,
                SquaresDb = _baseGrid
            };

            using (var context = new SudokuDbContext(options))
            {
                _validator = new SetCellValueValidator(context);
                var service = new SetCellValueHandler(context, _mapper, _validator, _logger);
                await context.Sessions.AddAsync(sessionDb);
                await context.SaveChangesAsync(); ;

                var request = new SetCellValueCommand()
                {
                    Id = 1,
                    Value = 1,
                    SessionId = 1
                };

                await service.AddError(request, CancellationToken.None);

                Assert.AreEqual(1, context.Sessions.First(x => x.Id == request.SessionId).Error);
            }
        }

        [Test]
        public void Test_SetCellValueHandler_IsOver_True()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_SetCellValueHandler_IsOver")
                .Options;

            var squaresDb = _baseGrid;


            using (var context = new SudokuDbContext(options))
            {
                _validator = new SetCellValueValidator(context);
                var service = new SetCellValueHandler(context, _mapper, _validator, _logger);


                var result = service.IsOver(squaresDb);

                Assert.IsTrue(result);
            }
        }


        [Test]
        public void Test_SetCellValueHandler_IsOver_False()
        {
            var options = new DbContextOptionsBuilder<SudokuDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_SetCellValueHandler_IsOver")
                .Options;

            var squaresDb = _baseGrid;
            squaresDb[0].Value = 0;

            using (var context = new SudokuDbContext(options))
            {
                _validator = new SetCellValueValidator(context);
                var service = new SetCellValueHandler(context, _mapper, _validator, _logger);


                var result = service.IsOver(squaresDb);

                Assert.IsFalse(result);
            }
        }
    }
}