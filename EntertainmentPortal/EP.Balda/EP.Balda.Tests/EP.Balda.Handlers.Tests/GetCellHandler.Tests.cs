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
    public class GetCellHandler_Tests
    {
        IMapper _mapper;
        
        [SetUp]
        public void Setup()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Cell>(It.IsAny<CellDb>())).Returns(new Cell());
            _mapper = mockMapper.Object;
        }

        [Test]
        public async Task GetCellHandler_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetCellHandler_Handle_NormalData")
                .Options;

            var request = new GetCell()
            {
                Id = 1
            };

            Maybe<Cell> result;

            var cellDb = new CellDb()
            {
                Id = 1,
                MapId = 1,
                X = 1,
                Y = 1
            };

            using (var context = new BaldaGameDbContext(options))
            {
                await context.Cells.AddAsync(cellDb);
                await context.SaveChangesAsync();
                var service = new GetCellHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.HasValue);
            }
        }

        [Test]
        public async Task GetAllPlayers_Handle_NotValidData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllPlayers_Handle_NotValidData")
                .Options;

            var request = new GetCell()
            {
                Id = 1
            };

            Maybe<Cell> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new GetCellHandler(context, _mapper);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsFalse(result.HasValue);
            }
        }
    }
}
