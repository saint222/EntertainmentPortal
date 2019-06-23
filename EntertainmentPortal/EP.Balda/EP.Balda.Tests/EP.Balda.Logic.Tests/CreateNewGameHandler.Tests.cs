//using AutoMapper;
//using EP.Balda.Data.Context;
//using EP.Balda.Data.Models;
//using EP.Balda.Logic.Handlers;
//using EP.Balda.Logic.Models;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Linq;

//namespace EP.Balda.Tests
//{
//    [TestFixture]
//    public class CreateNewGameHandlerTests
//    {
//        [SetUp]
//        public void Setup()
//        {
//            var context = new BaldaGameDbContext();
//            var dbSet = new Mock<DbSet<GameDb>>();

//            var games = new List<GameDb>
//            {
//                new GameDb { Id = 1, InitWord = "hair", MapId = 1 },
//                new GameDb { Id = 2, InitWord = "tomato", MapId = 2 },
//                new GameDb { Id = 3, InitWord = "google", MapId = 3 }
//            };

//            IQueryable<GameDb> queryableList = games.AsQueryable();

//            dbSet.As<IQueryable<GameDb>>().Setup(m => m.Provider).Returns(queryableList.Provider);
//            dbSet.As<IQueryable<GameDb>>().Setup(m => m.Expression).Returns(queryableList.Expression);
//            dbSet.As<IQueryable<GameDb>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
//            dbSet.As<IQueryable<GameDb>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

//            context.Students = dbSet.Object;

//            var mockMapper = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new AutoMapperProfile());
//            });
//            var mapper = mockMapper.CreateMapper();

//        }
        
//        [Test]
//        public void TestIsEmptyFalse()
//        {
//            var cell = new Cell(2, 1)
//            {
//                Letter = 'F'
//            };
//            var result = cell.IsEmpty();
//            Assert.IsFalse(result);
//        }

//        [Test]
//        public void TestIsEmptyTrue()
//        {
//            var cell = new Cell(2, 1);
//            var result = cell.IsEmpty();
//            Assert.IsTrue(result);
//        }
//    }
//}