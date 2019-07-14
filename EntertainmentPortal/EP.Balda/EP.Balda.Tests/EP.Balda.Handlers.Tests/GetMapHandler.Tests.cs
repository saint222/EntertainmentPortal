using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Handlers;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class GetMapHandler_Tests
    {
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Map>(It.IsAny<MapDb>())).Returns(new Map());
            _mapper = mockMapper.Object;
        }

        [Test]
        public async Task GetMapHandler_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMapHandler_Handle_NormalData")
                .Options;

            var request = new GetMap()
            {
                Id = 1
            };

            Maybe<Map> result;

            var mapDb = new MapDb()
            {
                Id = 1,
                Size = 3
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.Maps.AddAsync(mapDb);
                await context.SaveChangesAsync();
                var service = new GetMapHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.HasValue);
            }
        }

        [Test]
        public async Task GetMapHandler_Handle_NotValidData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMapHandler_Handle_NotValidData")
                .Options;

            var request = new GetMap()
            {
                Id = 1
            };

            Maybe<Map> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new GetMapHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.HasValue);
            }
        }
    }
}
