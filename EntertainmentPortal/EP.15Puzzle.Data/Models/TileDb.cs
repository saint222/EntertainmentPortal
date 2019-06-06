using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    public class TileDb
    {
        public int Id { get; set; }
        public int Pos { get; set; }
        public int Num { get; set; }

        

        public TileDb(int num)
        {
            Num = num % 16;
            Pos = num;
        }
    }
}
