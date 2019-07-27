using EP.SeaBattle.Common.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Models
{
    public class Ship
    {
        public Ship()
        {

        }

        public Ship(Game game, Player player, IEnumerable<Cell> cells)
        {
            Cells = cells;
            Rank = (ShipRank)cells.Count();
            Game = game;
            Player = player;
        }

        /// <summary>
        /// Ship rank
        /// </summary>
        public ShipRank Rank { get; }

        /// <summary>
        /// Collection of ship cells
        /// </summary>
        public IEnumerable<Cell> Cells { get; }

        /// <summary>
        /// Inform is all cells destroyed
        /// </summary>
        public bool IsAlive { get => Cells.Any(a => a.Status == Common.Enums.CellStatus.Alive); }

        public Player Player { get; set; }

        public Game Game { get; set; }
    }
}
