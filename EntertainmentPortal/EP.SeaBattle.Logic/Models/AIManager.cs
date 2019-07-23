using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EP.SeaBattle.Logic.Models
{
    public class AIManager
    {
        const int MAX_ITERATION = 1000;
        private readonly Game _game;
        private readonly Player _player;
        private readonly IEnumerable<Ship> _ships;
        private readonly IEnumerable<Shot> _shots;

        public ShipsManager ShipsManager { get; }

        public AIManager()
        {

        }

        public AIManager(Game game, Player player, IEnumerable<Ship> ships, IEnumerable<Shot> prevShots)
        {
            _game = game;
            _player = player;
            _ships = ships;
            _shots = prevShots;
            ShipsManager = new ShipsManager(_game, _player, _ships);          
        }

        public bool GenerateShips()
        {
            //Ship shipTemp;
            //ShipsManager.TryAddShip(0, 0, ShipOrientation.Verctical, ShipRank.Four, out shipTemp);
            Random rand = new Random();
            int count = 0;
           
            while (!ShipsManager.IsFull && count < MAX_ITERATION)
            {
                Ship ship;
                ShipsManager.TryAddShip((byte)rand.Next(0, 10), (byte)rand.Next(0, 10), (ShipOrientation)rand.Next(0, 2), (ShipRank) rand.Next(1, 5), out ship);
                count++;
            }
            return ShipsManager.IsFull;
        }

        public bool TryShoot(out Shot shot)
        {
            Random random = new Random();
            var shotManager = new ShotManager(_game, _player, _ships, _shots);
            int count = 0;
            while(count < MAX_ITERATION)
            {
                byte x = (byte)random.Next(0, 10);
                byte y = (byte)random.Next(0, 10);
                if (shotManager.TryShoot(x, y))
                {
                    shot = shotManager.Shoot(x, y);
                    return true;
                }

                count++;
            }
            shot = new Shot();
            return false;
        }
    }
}
