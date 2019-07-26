using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Handlers;
using EP.Balda.Logic.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class AddWordToPlayerHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
        }

        [Test]
        public void TestIsWordCorrect_True()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestIsWordCorrect_True")
                .Options;

            bool isCorrect;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                var cell1 = new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new Cell() { Id = 2, MapId = 1, X = 0, Y = 1, Letter = 'o' };
                var cell3 = new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<Cell>() { cell1, cell2, cell3 };

                context.Words.Add(new WordDb() { Word = "dog" });
                context.SaveChanges();
                isCorrect = service.IsWordCorrect(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(isCorrect);
            }
        }

        [Test]
        public void TestIsWordCorrect_False()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestIsWordCorrect_False")
                .Options;

            bool isCorrect;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                var cell1 = new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new Cell() { Id = 2, MapId = 1, X = 0, Y = 2, Letter = 'o' };
                var cell3 = new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<Cell>() { cell1, cell2, cell3 };

                context.Words.Add(new WordDb() { Id = 1, Word = "dog" });
                context.SaveChanges();
                isCorrect = service.IsWordCorrect(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(isCorrect);
            }
        }

        [Test]
        public void TestGetSelectedWord()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestGetSelectedWord")
                .Options;

            string word;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                var cell1 = new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new Cell() { Id = 2, MapId = 1, X = 0, Y = 2, Letter = 'o' };
                var cell3 = new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<Cell>() { cell1, cell2, cell3 };

                context.SaveChanges();
                word = service.GetSelectedWord(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.AreEqual("dog", word);
            }
        }

        [Test]
        public async Task TestAddWordToPlayer_Handle_PlayerDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestHandle_PlayerDoesNotExist")
                .Options;

            var request = new AddWordToPlayerCommand()
            {
                PlayerId = "1",
                CellsThatFormWord = new List<Cell>() {
                    new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' },
                    new Cell() { Id = 2, MapId = 1, X = 0, Y = 1, Letter = 'o' },
                    new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' }
            },
                GameId = 1
            };

            Result<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.IsSuccess);
            }
        }


        [Test]
        public async Task TestAddWordToPlayer_Handle_PlayerGameDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestHandle_PlayerGameDoesNotExist")
                .Options;

            var request = new AddWordToPlayerCommand()
            {
                PlayerId = "1",
                CellsThatFormWord = new List<Cell>() {
                    new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' },
                    new Cell() { Id = 2, MapId = 1, X = 0, Y = 1, Letter = 'o' },
                    new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' }
                },
                GameId = 1
            };

            var playerDb = new PlayerDb()
            {
                Created = DateTime.UtcNow,
                UserName = "Login"
            };

            Result<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                await context.AddAsync(playerDb);

                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.IsSuccess);
            }
        }

        [Test]
        public async Task TestAddWordToPlayer_Handle_WordIsInitial()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestHandle_WordIsInitial")
                .Options;

            var request = new AddWordToPlayerCommand()
            {
                CellsThatFormWord = new List<Cell>() {
                    new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' },
                    new Cell() { Id = 2, MapId = 1, X = 0, Y = 1, Letter = 'o' },
                    new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' }
                },
                GameId = 1
            };

            var playerDb = new PlayerDb()
            {
                Created = DateTime.UtcNow,
                UserName = "Login"
            };

            var playerGame = new PlayerGame()
            {
                PlayerId = "1"
            };

            var mapDb = new MapDb()
            {
                Size = 3
            };

            var gameDb = new GameDb()
            {
                InitWord = "dog",
                MapId = 1
            };

            Result<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                await context.Users.AddAsync(playerDb);
                await context.Maps.AddAsync(mapDb);
                await context.Games.AddAsync(gameDb);
                await context.PlayerGames.AddAsync(playerGame);
                await context.SaveChangesAsync();
                var cell1 = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new CellDb() { Id = 2, MapId = 1, Map = mapDb, X = 0, Y = 1, Letter = 'o' };
                var cell3 = new CellDb() { Id = 3, MapId = 1, Map = mapDb, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<CellDb>() { cell1, cell2, cell3 };
                await context.Cells.AddRangeAsync(cells);

                context.SaveChanges();

                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.IsSuccess);
            }
        }

        [Test]
        public async Task TestAddWordToPlayer_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAddWordToPlayer_Handle_NormalData")
                .Options;

            var request = new AddWordToPlayerCommand()
            {
                PlayerId = "1",
                CellsThatFormWord = new List<Cell>() {
                    new Cell() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' },
                    new Cell() { Id = 2, MapId = 1, X = 0, Y = 1, Letter = 'o' },
                    new Cell() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' }
                },
                GameId = 1
            };

            var playerDb = new PlayerDb()
            {
                Id = "1",
                Created = DateTime.UtcNow,
                UserName = "Login"
            };

            var playerGame = new PlayerGame()
            {
                PlayerId = "1",
                GameId = 1
            };

            var mapDb = new MapDb()
            {
                Id = 1,
                Size = 3
            };

            var gameDb = new GameDb()
            {
                Id = 1,
                InitWord = "god",
                MapId = 1
            };

            var wordDb = new WordDb()
            {
                Id = 1,
                Word = "dog"
            };

            var cell1 = new CellDb() { Id = 1, MapId = 1, Map = mapDb, X = 0, Y = 0, Letter = 'd' };
            var cell2 = new CellDb() { Id = 2, MapId = 1, Map = mapDb, X = 0, Y = 1, Letter = 'o' };
            var cell3 = new CellDb() { Id = 3, MapId = 1, Map = mapDb, X = 1, Y = 1, Letter = 'g' };

            var cells = new List<CellDb>() { cell1, cell2, cell3 };

            Result<Game> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                await context.Users.AddAsync(playerDb);
                await context.Maps.AddAsync(mapDb);
                await context.Games.AddAsync(gameDb);
                await context.PlayerGames.AddAsync(playerGame);
                await context.SaveChangesAsync();

                await context.Cells.AddRangeAsync(cells);
                await context.Words.AddAsync(wordDb);
                context.SaveChanges();

                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.IsSuccess);
            }
        }

        [Test]
        public void TestIsGameOver_True()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestIsGameOver_True")
                .Options;

            bool isOver;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                var cell1 = new CellDb() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new CellDb() { Id = 2, MapId = 1, X = 0, Y = 2, Letter = 'o' };
                var cell3 = new CellDb() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = 'g' };

                var cells = new List<CellDb>() { cell1, cell2, cell3 };

                isOver = service.IsGameOver(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(isOver);
            }
        }

        [Test]
        public void TestIsGameOver_False()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestIsGameOver_False")
                .Options;

            bool isOver;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new AddWordToPlayerHandler(context, _mapper);
                var cell1 = new CellDb() { Id = 1, MapId = 1, X = 0, Y = 0, Letter = 'd' };
                var cell2 = new CellDb() { Id = 2, MapId = 1, X = 0, Y = 2, Letter = 'o' };
                var cell3 = new CellDb() { Id = 3, MapId = 1, X = 1, Y = 1, Letter = null };

                var cells = new List<CellDb>() { cell1, cell2, cell3 };

                isOver = service.IsGameOver(cells);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(isOver);
            }
        }
    }
}
