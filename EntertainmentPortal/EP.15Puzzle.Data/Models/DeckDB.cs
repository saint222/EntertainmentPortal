using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    public class DeckDB
    {
        /// <summary>
        /// UserId property
        /// </summary>
        /// <value>Represents ID of user the deck belongs</value>
        public int UserId { get; set; }

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

        /// <summary>
        /// Tiles property
        /// </summary>
        /// <remarks>
        ///Tiles[0] represents empty tile
        /// </remarks>
        /// <value>Represents a list with positions as tile numbers and values as their relevant positions on deck</value>
        public List<int> Tiles { get; set; }
        
    }
}
