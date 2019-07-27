using EP.SeaBattle.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EP.SeaBattle.Logic.Models
{
    public class Ship
    {
        public string Id { get; set; }
        /// <summary>
        /// Ship rank
        /// </summary>
        public ShipRank Rank { get; set; }

        /// <summary>
        /// Collection of ship cells
        /// </summary>
        public ICollection<Cell> Cells { get; set; }

        /// <summary>
        /// Inform is all cells destroyed
        /// </summary>
        public bool IsAlive { get => Cells.Any(c => c.IsAlive == true); }
        
    }
}
