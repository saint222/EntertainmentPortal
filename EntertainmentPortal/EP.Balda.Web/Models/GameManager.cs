using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Balda.Models
{
    public class GameManager
    {
        public Player[] Players { get; set; }
        public Field Field { get; set; }
        public Word InitialWord { get; set; } // start word in the middle of the field

        public GameManager(Player player1, Player player2, Word initialWord)
        {
            Players = new Player[2];
            Players[0] = player1;
            Players[1] = player2;

            Field = new Field();
            int center = Field.Size / 2;

            for (int i = 0; i < Field.Size; i++) //to add word to start
            {
                Field.Cells[center, i].Letter = initialWord.Letters[i];
            }
        }
    }
}
