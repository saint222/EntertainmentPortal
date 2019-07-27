using EP.SeaBattle.Logic.Models;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Tests
{
    [TestFixture]
    public class FieldManagerTests
    {
        List<Ship> ships;
        Player player;
        Game game;
        FieldManager fieldManager;
        const byte SIZE = 10;
        [SetUp]
        public void SetUp()
        {
            ships = new List<Ship>();
            player = new Player();
            game = new Game();
            List<Cell> cells1 = new List<Cell>()
            {
                new Cell(0, 0, CellStatus.Alive),
                new Cell(0, 1, CellStatus.Alive),
                new Cell(0, 2, CellStatus.Destroyed),
            };
            ships.Add(new Ship(game, player, cells1));
            List<Cell> cells2 = new List<Cell>()
            {
                new Cell(6, 9, CellStatus.Alive),
                new Cell(7, 9, CellStatus.Destroyed),
                new Cell(8, 9, CellStatus.Alive),
                new Cell(9, 9, CellStatus.Alive)
            };
            ships.Add(new Ship(game, player, cells2));
            List<Cell> cells3 = new List<Cell>()
            {
                new Cell(9, 0, CellStatus.Alive),
                new Cell(9, 1, CellStatus.Destroyed),
                new Cell(9, 2, CellStatus.Alive)
            };
            ships.Add(new Ship(game, player, cells3));
            List<Cell> cells4 = new List<Cell>()
            {
                new Cell(0, 9, CellStatus.Alive)
            };
            ships.Add(new Ship(game, player, cells4));
            List<Cell> cells5 = new List<Cell>()
            {
                new Cell(5, 4, CellStatus.Alive),
                new Cell(5, 5, CellStatus.Alive)
            };
            ships.Add(new Ship(game, player, cells5));
            fieldManager = new FieldManager(ships);
        }

        [Test]
        public void Initialize_Field_Test()
        {
            Cell[,] expected = new Cell[SIZE, SIZE];
            for (byte x = 0; x < SIZE; x++)
            {
                for (byte y = 0; y < SIZE; y++)
                {
                    expected[x, y] = new Cell(x, y, CellStatus.None);
                }
            }
            #region Set status
            //Ship cells
            expected[0, 0].Status = CellStatus.Alive;
            expected[0, 1].Status = CellStatus.Alive;

            expected[9, 0].Status = CellStatus.Alive;
            expected[9, 2].Status = CellStatus.Alive;

            expected[6, 9].Status = CellStatus.Alive;
            expected[8, 9].Status = CellStatus.Alive;
            expected[9, 9].Status = CellStatus.Alive;

            expected[0, 9].Status = CellStatus.Alive;

            expected[5, 4].Status = CellStatus.Alive;
            expected[5, 5].Status = CellStatus.Alive;
            //Destroyed cells
            expected[0, 2].Status = CellStatus.Destroyed;
            expected[9, 1].Status = CellStatus.Destroyed;
            expected[7, 9].Status = CellStatus.Destroyed;
            //Forbidden cells
            expected[0, 3].Status = CellStatus.Forbidden;
            expected[1, 0].Status = CellStatus.Forbidden;
            expected[1, 1].Status = CellStatus.Forbidden;
            expected[1, 2].Status = CellStatus.Forbidden;
            expected[1, 3].Status = CellStatus.Forbidden;

            expected[8, 0].Status = CellStatus.Forbidden;
            expected[8, 1].Status = CellStatus.Forbidden;
            expected[8, 2].Status = CellStatus.Forbidden;
            expected[8, 3].Status = CellStatus.Forbidden;
            expected[9, 3].Status = CellStatus.Forbidden;

            expected[0, 8].Status = CellStatus.Forbidden;
            expected[1, 8].Status = CellStatus.Forbidden;
            expected[1, 9].Status = CellStatus.Forbidden;

            expected[5, 8].Status = CellStatus.Forbidden;
            expected[5, 9].Status = CellStatus.Forbidden;
            expected[6, 8].Status = CellStatus.Forbidden;
            expected[7, 8].Status = CellStatus.Forbidden;
            expected[8, 8].Status = CellStatus.Forbidden;
            expected[9, 8].Status = CellStatus.Forbidden;

            expected[4, 3].Status = CellStatus.Forbidden;
            expected[4, 4].Status = CellStatus.Forbidden;
            expected[4, 5].Status = CellStatus.Forbidden;
            expected[4, 6].Status = CellStatus.Forbidden;
            expected[5, 3].Status = CellStatus.Forbidden;
            expected[5, 6].Status = CellStatus.Forbidden;
            expected[6, 3].Status = CellStatus.Forbidden;
            expected[6, 4].Status = CellStatus.Forbidden;
            expected[6, 5].Status = CellStatus.Forbidden;
            expected[6, 6].Status = CellStatus.Forbidden;
            #endregion

            var fieldManager = new FieldManager(ships);

            CollectionAssert.AreEqual(expected, fieldManager.Cells);
        }

        [Test]
        public void Add_Ship_Out_Of_Range_Test()
        {
            var actual = fieldManager.AddShip(9, 5, ShipOrientation.Horizontal, ShipRank.Two);
            Assert.AreEqual(false, actual, "Ship was added out of range");
        }

        [Test]
        public void Add_Ship_On_Existing_Ship()
        {
            var actual = fieldManager.AddShip(0, 0, ShipOrientation.Horizontal, ShipRank.Two);
            Assert.AreEqual(false, actual, "Ship was added at exist ship");
        }

        [Test]
        public void Add_Ship_On_Forbidden_Cell()
        {
            var actual = fieldManager.AddShip(3, 3, ShipOrientation.Horizontal, ShipRank.Two);
            Assert.AreEqual(false, actual, "Ship was added on forbidden cell");
        }

        [Test]
        public void Add_Ship_Success()
        {
            var actual = fieldManager.AddShip(0, 4, ShipOrientation.Verctical, ShipRank.Two);
            Assert.AreEqual(true, actual, "Ship was not added");
        }
    }
}
