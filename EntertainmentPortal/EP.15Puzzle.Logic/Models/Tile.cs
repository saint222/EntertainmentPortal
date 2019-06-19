using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Logic.Models
{
    public class Tile
    {
        /// <summary>
        /// PosX property
        /// </summary>
        /// <value>Represents a horizontal position on deck</value>
        public int PosX { get; set; }

        /// <summary>
        /// PosY property
        /// </summary>
        /// <value>Represents a vertical position on deck</value>
        public int PosY { get; set; }

        /// <summary>
        /// Tile constructor
        /// </summary>
        /// <param name="pos">Number of current position on deck</param>
        public Tile(int pos)
        {
            
            PosX = (pos - 1) % 4 + 1;
            PosY = (pos - 1) / 4 + 1;
        }
    }
}
