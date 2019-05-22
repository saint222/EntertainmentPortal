using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Logic.Models
{
    public class Tile
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Tile(int pos)
        {
            
            PosX = (pos - 1) % 4 + 1;
            PosY = (pos - 1) / 4 + 1;
        }
    }
}
