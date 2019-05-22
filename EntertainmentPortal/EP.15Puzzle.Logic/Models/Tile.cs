using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Logic.Models
{
    public class Tile
    {
        /// <summary>
        /// PosX describes tile column on 4x4 deck
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// PosY describes tile row on 4x4 deck
        /// </summary>
        public int PosY { get; set; }

        /// <summary>
        /// PosX and PosY describes tile position on 4x4 deck
        /// </summary>
        /// <param name="pos">current position [1...16]</param>
        public Tile(int pos)
        {
            
            PosX = (pos - 1) % 4 + 1;
            PosY = (pos - 1) / 4 + 1;
        }
    }
}
