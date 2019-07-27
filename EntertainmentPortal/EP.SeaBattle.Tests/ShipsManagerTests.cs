//using System;
//using System.Collections.Generic;
//using System.Text;
//using EP.SeaBattle.Common.Enums;
//using EP.SeaBattle.Logic.Models;
//using NUnit.Framework;

//namespace EP.SeaBattle.Tests
//{
//    class ShipsManagerTests
//    {
//        List<Ship> ships;
//        ShipsManager shipsManager;
//        Player player;
//        Game game;
//        [SetUp]
//        public void SetUp()
//        {
//            player = new Player();
//            game = new Game();
//            ships = new List<Ship>();
//            List<Cell> cells1 = new List<Cell>()
//            {
//                new Cell(0, 0, CellStatus.Alive),
//                new Cell(0, 1, CellStatus.Alive),
//                new Cell(0, 2, CellStatus.Destroyed),
//            };
//            ships.Add(new Ship(game, player, cells1));
//            List<Cell> cells2 = new List<Cell>()
//            {
//                new Cell(6, 9, CellStatus.Alive),
//                new Cell(7, 9, CellStatus.Destroyed),
//                new Cell(8, 9, CellStatus.Alive),
//                new Cell(9, 9, CellStatus.Alive)
//            };
//            ships.Add(new Ship(game, player, cells2));
//            List<Cell> cells3 = new List<Cell>()
//            {
//                new Cell(9, 0, CellStatus.Alive),
//                new Cell(9, 1, CellStatus.Destroyed),
//                new Cell(9, 2, CellStatus.Alive)
//            };
//            ships.Add(new Ship(game, player, cells3));
//            List<Cell> cells4 = new List<Cell>()
//            {
//                new Cell(0, 9, CellStatus.Alive)
//            };
//            ships.Add(new Ship(game, player, cells4));
//            List<Cell> cells5 = new List<Cell>()
//            {
//                new Cell(5, 4, CellStatus.Alive),
//                new Cell(5, 5, CellStatus.Alive)
//            };
//            ships.Add(new Ship(game, player, cells5));

//            shipsManager = new ShipsManager(game, player, ships);
//        }

//        [Test]
//        public void Add_Third_Ship_Rank_Three()
//        {
//            var actual = shipsManager.AddShip(0, 7, ShipOrientation.Horizontal, ShipRank.Three);
//            Assert.AreEqual(false, actual, "Third ship rank 3 was added");
//        }

//        [Test]
//        public void Add_Ship_Rank_Two()
//        {
//            var actual = shipsManager.AddShip(0, 7, ShipOrientation.Horizontal, ShipRank.Two);
//            Assert.AreEqual(true, actual, "Ship wasn't added");
//        }
//    }
//}
