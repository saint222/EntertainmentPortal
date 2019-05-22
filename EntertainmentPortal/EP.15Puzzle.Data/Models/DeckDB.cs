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
        public int Id { get; set; }
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
        /// Creating new deck generates 15 Tiles and 1 Empty. Then shuffle them.
        /// </summary>
        public DeckDB()
        {
            Tiles = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
            
            Random random = new Random();
            for (int i = Tiles.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                var temp = Tiles[i];
                Tiles[i] = Tiles[j];
                Tiles[j] = temp;
            }
            Tiles.Insert(0,16);
            Score = 0;
            Victory = false;
        }
    }
}
