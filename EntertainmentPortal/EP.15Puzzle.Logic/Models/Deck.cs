using System;
using System.Collections.Generic;

namespace EP._15Puzzle.Logic
{
    public class Deck
    {
        /// <summary>
        /// Stores current state of tiles
        /// </summary>
        public IEnumerable<int> Tiles { get; set; }
        /// <summary>
        /// Count of turns already did
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Flag to set if deck state is winning
        /// </summary>
        public bool Victory { get; set; }
    }
}
