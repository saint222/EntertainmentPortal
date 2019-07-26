using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Handlers;
using EP.Balda.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class GetPlayersWordsHandler_Tests
    {
        [Test]
        public async Task GetPlayersWordsHandler_Handle_NormalData_RightOrder()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlayersWordsHandler_Handle_NormalData_RightOrder")
                .Options;

            var request = new GetPlayersWords()
            {
                GameId = 1,
                PlayerId = "1"
            };

            List<string> result;

            var playerWord1 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = false,
                Word = new WordDb { Id = 1, Word = "god" }
            };
            var playerWord2 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = false,
                Word = new WordDb { Id = 2, Word = "dog" }
            };
            var playerWord3 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = true,
                Word = new WordDb { Id = 3, Word = "cat" }
            };
            
            using (var context = new BaldaGameDbContext(options))
            {
                await context.PlayerWords.AddAsync(playerWord1);
                await context.PlayerWords.AddAsync(playerWord2);
                await context.PlayerWords.AddAsync(playerWord3);
                await context.SaveChangesAsync();
                var service = new GetPlayersWordsHandler(context);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result[0] == "god");
                Assert.IsTrue(result[1] == "dog");
                Assert.IsTrue(result.Count == 2);
            }
        }

        [Test]
        public async Task GetPlayersWordsHandler_Handle_NormalData_Contains()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlayersWordsHandler_Handle_NormalData_Contains")
                .Options;

            var request = new GetPlayersWords()
            {
                GameId = 1,
                PlayerId = "1"
            };

            List<string> result;

            var playerWord1 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = false,
                Word = new WordDb { Id = 1, Word = "god" }
            };
            var playerWord2 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = false,
                Word = new WordDb { Id = 2, Word = "dog" }
            };
            var playerWord3 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = true,
                Word = new WordDb { Id = 3, Word = "cat" }
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.PlayerWords.AddAsync(playerWord1);
                await context.PlayerWords.AddAsync(playerWord2);
                await context.PlayerWords.AddAsync(playerWord3);
                await context.SaveChangesAsync();
                var service = new GetPlayersWordsHandler(context);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.Contains("god"));
                Assert.IsTrue(result.Contains("dog"));
                Assert.IsTrue(result.Count == 2);
            }
        }

        [Test]
        public async Task GetPlayersWordsHandler_Handle_NoWords()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlayersWordsHandler_Handle_NoWords")
                .Options;

            var request = new GetPlayersWords()
            {
                GameId = 1,
                PlayerId = "1"
            };

            List<string> result;

            var playerWord3 = new PlayerWord
            {
                PlayerId = "1",
                GameId = 1,
                IsChosenByOpponnent = true,
                Word = new WordDb { Id = 3, Word = "cat" }
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.PlayerWords.AddAsync(playerWord3);
                await context.SaveChangesAsync();
                var service = new GetPlayersWordsHandler(context);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.Count == 0);
            }
        }
    }
}
