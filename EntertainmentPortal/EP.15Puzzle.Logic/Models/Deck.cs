using System.Collections.Generic;

namespace EP._15Puzzle.Logic.Models
{
    public class Deck
    {
        
        public int Score { get; set; }
        public bool Victory { get; set; }
        public int Size { get; set; }
        public ICollection<Tile> Tiles { get; set; }

        
        
    }
}
