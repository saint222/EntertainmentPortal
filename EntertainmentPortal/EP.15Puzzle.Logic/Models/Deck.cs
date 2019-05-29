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
        /// UserId property
        /// </summary>
        /// <value>Represents ID of user the deck belongs</value>
        public int UserID { get; set; }

        /// <summary>
        /// Tiles property
        /// </summary>
        /// <remarks>
        ///Tiles[0] represents empty tile
        /// </remarks>
        /// <value>Represents a list with positions as tile numbers and values as their relevant positions on deck</value>
        public IEnumerable<int> Tiles { get; set; }

        /// <summary>
        /// Score property
        /// </summary>
        /// <value>Represents count of turns user already did</value>
        public int Score { get; set; }

        /// <summary>
        /// Victory property
        /// </summary>
        /// <value>Flag represents winning state of deck</value>
        public bool Victory { get; set; }
        
    }
}
