using System;
using System.Collections.Generic;
using System.Text;

namespace EP._15Puzzle.Data.Models
{
    public class DeckDB
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public bool Victory { get; set; }
        /// <summary>
        /// [i] - name, value - position
        /// </summary>
        public List<int> Tiles { get; set; }

        public DeckDB()
        {
            Tiles = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
            //unsorting
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
