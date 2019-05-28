using EP._15Puzzle.Data;
using System;
using System.Collections.Generic;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;

namespace EP._15Puzzle.Logic
{
    public class Deck
    {
        /// <summary>
        /// ID of User
        /// </summary>
        public int UserID { get; set; }
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
