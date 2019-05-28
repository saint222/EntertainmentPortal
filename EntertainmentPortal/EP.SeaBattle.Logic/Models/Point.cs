using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Models
{ 
    public class Point
    {
        /// <summary>
        /// X
        /// </summary>
        /// <remarks>
        /// Coordinate X
        /// </remarks>
        public int X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        /// <remarks> Coordinate Y
        /// </remarks> 
        public int Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// true if a ship is located at a point
        /// </remarks>
        public bool IsShip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// true if the user cannot place a ship at a point or shoot
        /// </remarks>
        public bool IsForbidden { get; set; }
    }
}
