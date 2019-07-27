//using EP.SeaBattle.Logic.Models;
//using NUnit.Framework;
//using System.Collections.Generic;
//using EP.SeaBattle.Common.Enums;

//namespace EP.SeaBattle.Tests
//{
//    public class ShipTests
//    {
//        private List<Cell> cells;
//        Player player;
//        Game game;
//        [SetUp]
//        public void Setup()
//        {
//            player = new Player();
//            game = new Game();
//            cells = new List<Cell>();
//            Cell cell1 = new Cell(0, 0, CellStatus.Alive);
//            Cell cell2 = new Cell(0, 1, CellStatus.Alive);
//            Cell cell3 = new Cell(0, 2, CellStatus.Alive);

//            cells.Add(cell1);
//            cells.Add(cell2);
//            cells.Add(cell3);
//        }

//        [Test]
//        public void Ship_Rank_Three_Test()
//        {
//            var ship = new Ship(game, player, cells);
//            Assert.AreEqual(ShipRank.Three, ship.Rank);
//        }

//        [Test]
//        public void Ship_Rank_One_Test()
//        {
//            cells.RemoveAt(0);
//            cells.RemoveAt(1);
//            var ship = new Ship(game, player, cells);
//            Assert.AreEqual(ShipRank.One, ship.Rank);
//        }

//        [Test]
//        public void Ship_IsAlive_True_Test()
//        {
//            cells[0].Status = CellStatus.Destroyed;
//            var ship = new Ship(game, player, cells);

//            Assert.AreEqual(true, ship.IsAlive);
//        }

//        [Test]
//        public void Ship_IsAlive_False_Test()
//        {
//            foreach (var cell in cells)
//            {
//                cell.Status = CellStatus.Destroyed;
//            }

//            var ship = new Ship(game, player, cells);
//            Assert.AreEqual(false, ship.IsAlive);
//        }
//    }
//}