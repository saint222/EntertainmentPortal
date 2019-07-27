using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.SeaBattle.Tests
{
    [TestFixture]
    public class AIManagerTests
    {
        List<Ship> ships;
        Player player;
        Game game;
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
        }

        [Test]
        public void Generate_Ships_Test()
        {
            AIManager aiManager = new AIManager(game, player, Enumerable.Empty<Ship>(), Enumerable.Empty<Shot>());
            var answer = aiManager.GenerateShips();
            Assert.AreEqual(true, answer, "cannot generate ships");
        }

        [Test]
        public void Try_Generate_Shot_Null_Test()
        {
            game.Status = GameStatus.Started;
            game.PlayerAllowedToMove = player;
            List<Shot> shots = new List<Shot>();
            shots.Add(new Shot(player.Id, game.Id, 0, 0, CellStatus.ShootWithoutHit));
            shots.Add(new Shot(player.Id, game.Id, 1, 0, CellStatus.ShootWithoutHit));
            shots.Add(new Shot(player.Id, game.Id, 0, 1, CellStatus.Destroyed));
            AIManager aiManager = new AIManager(game, player, Enumerable.Empty<Ship>(), shots);
            Shot shot;
            var actual = aiManager.TryShoot(out shot);
            Assert.AreEqual(true, actual);
            Assert.NotNull(shot);
        }
    }
}
