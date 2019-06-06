using EP._15Puzzle.Data;
using System;
using System.Collections.Generic;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;

namespace EP._15Puzzle.Logic
{
    public class Deck
    {
        public int UserId { get; set; }

        public int Score { get; set; }
        public bool Victory { get; set; }

        public ICollection<Tile> Tiles { get; set; }
        public Tile EmptyTile { get; set; }

        
        
    }
}
