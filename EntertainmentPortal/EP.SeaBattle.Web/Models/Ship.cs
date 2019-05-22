using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Web.Models
{
    public class Ship
    {
        private int _shipRank;
        private ShipOrientation _shipOrientation;
        private Point _startPosition;

        public int ShipRank { get => _shipRank; set => _shipRank = value; }

        public ShipOrientation ShipOrientation { get => _shipOrientation; set => _shipOrientation = value; }

        public Point StartPosition { get => _startPosition; set => _startPosition = value; }
    }
}
