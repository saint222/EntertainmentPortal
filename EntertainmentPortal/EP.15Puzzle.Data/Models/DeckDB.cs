using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    public class DeckDB
    {
        /// <summary>
        /// Id of user the deck belongs
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Count of turns already did
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Flag to set if deck state is winning
        /// </summary>
        public bool Victory { get; set; }
        /// <summary>
        /// deck of tiles, where position at List [i] - means № of each Tile, values means their relevant positions on deck
        /// </summary>
        public List<int> Tiles { get; set; }
        /// <summary>
        /// Creating new deck generates 15 Tiles and 1 Empty.
        /// </summary>
        
    }
}
