using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Models
{
    public class ShipsManager
    {
        private readonly Dictionary<ShipRank, byte> _shipsCount;
        private readonly Dictionary<ShipRank, byte> shipsRuleCount = new Dictionary<ShipRank, byte>
        {
            { ShipRank.One, SHIP_RANK_ONE_MAX_COUNT },
            { ShipRank.Two, SHIP_RANK_TWO_MAX_COUNT },
            { ShipRank.Three, SHIP_RANK_THREE_MAX_COUNT },
            { ShipRank.Four, SHIP_RANK_FOUR_MAX_COUNT },
        };

        public const byte SHIP_RANK_FOUR_MAX_COUNT = 1;
        public const byte SHIP_RANK_THREE_MAX_COUNT = 2;
        public const byte SHIP_RANK_TWO_MAX_COUNT = 3;
        public const byte SHIP_RANK_ONE_MAX_COUNT = 4;

        public const byte MAX_SHIPS_COUNT = 10;
        readonly Player _player;


        public ShipsManager(Player player)
        {

            _player = player;
            _shipsCount = new Dictionary<ShipRank, byte>(4)
            {
                { ShipRank.One, 0 },
                { ShipRank.Two, 0 },
                { ShipRank.Three, 0 },
                { ShipRank.Four, 0 }
            };

            foreach (var ship in player.Ships)
            {
                _shipsCount[ship.Rank] += 1;
            }

        }


        /// <summary>
        /// Returns true if all ships are set
        /// </summary>
        public bool IsFull
        {
            get
            {
                if (_player.Ships.Count() == MAX_SHIPS_COUNT)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Inform is all ships destroyed
        /// </summary>
        //public bool AllShipsDestroyed { get => _ships.Any(a => a.IsAlive); }

        /// <summary>
        /// Add ship
        /// </summary>
        /// <param name="ship">Ship</param>
        private bool AddShip(Ship ship)
        {

            var rank = ship.Rank;
            try
            {
                if (_shipsCount[rank] < shipsRuleCount[rank])
                {
                    _player.Ships.Add(ship);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Add ship
        /// </summary>
        /// <param name="x">x-coordinate of ship start cell</param>
        /// <param name="y">y-coordinate of ship start cell</param>
        /// <param name="shipOrientation">Orientation</param>
        /// <param name="rank">Rank</param>
        /// <returns></returns>

        public bool AddShip(byte x, byte y, ShipOrientation shipOrientation, ShipRank rank, out Ship ship)
        {
            FieldManager fieldManager = new FieldManager(_player.Ships);
            if (fieldManager.CheckShip(x, y, shipOrientation, rank))
            {
                ship = new Ship { PlayerId = _player.Id, Cells = GenerateCell(x, y, shipOrientation, rank), Rank = rank };
                return AddShip(ship);
            }
            ship = null;
            return false;
        }

        /// <summary>
        /// Delete ship
        /// </summary>
        /// <param name="ship">Ship</param>
        private bool DeleteShip(Ship ship)
        {
            //TODO Throw message if ship not found
            return _player.Ships.Remove(ship);
        }

        /// <summary>
        /// Generate cell for ship
        /// </summary>
        /// <param name="x">x-coordinate of ship start point</param>
        /// <param name="y">y-coordinate of ship start point</param>
        /// <param name="shipOrientation">Orientation</param>
        /// <param name="rank">Rank</param>
        /// <returns></returns>
        private ICollection<Cell> GenerateCell(byte x, byte y, ShipOrientation shipOrientation, ShipRank rank)
        {
            List<Cell> cells = new List<Cell>((byte)rank);
            for (byte i = 0; i < (byte)rank; i++)
            {
                if (shipOrientation == ShipOrientation.Horizontal)
                {
                    Cell cell = new Cell { X = Convert.ToByte(x + i), Y = y, IsAlive = true };
                    cells.Add(cell);
                }
                else
                {
                    Cell cell = new Cell { X = x, Y = Convert.ToByte(y + i), IsAlive = true };
                    cells.Add(cell);
                }
            }
            return cells;
        }
    }
}
