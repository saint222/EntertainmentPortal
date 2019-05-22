using System;
using System.Collections.Generic;

namespace EP._15Puzzle.Logic
{
    public class Deck
    {
        public IEnumerable<int> Tiles { get; set; }
        public int Score { get; set; }
        public bool Victory { get; set; }
    }
}
