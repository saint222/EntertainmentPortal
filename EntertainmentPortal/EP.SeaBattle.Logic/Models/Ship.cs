using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Models
{
    public class Ship
    {
        private int _shipRank;
        private ShipOrientation _shipOrientation;
        private Point _startPosition;

        /// <summary>
        /// Rank of ship
        /// </summary>
        /// <remarks> 
        /// 1, 2, 3 or 4
        /// </remarks>
        public int ShipRank { get => _shipRank; set => _shipRank = value; }

        /// <summary>
        /// Ship orientation
        /// </summary>
        /// <remarks>
        /// Vertical or horizontal
        /// </remarks>
        public ShipOrientation ShipOrientation { get => _shipOrientation; set => _shipOrientation = value; }

        /// <summary>
        /// First point of ship
        /// </summary>
        /// <remarks>
        /// Top point if vertical and left point if horizontal
        /// </remarks>
        public Point StartPosition { get => _startPosition; set => _startPosition = value; }
    }
}
