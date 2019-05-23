namespace EP.Balda.Models
{
    public class Game
    {
        public Player[] Players { get; set; }
        public Field Field { get; set; }
        public string InitialWord { get; set; } // start word in the middle of the field

        public Game(Player player1, Player player2, char[] initialWord)
        {
            Players = new Player[2];
            Players[0] = player1;
            Players[1] = player2;

            Field = new Field();
            var center = Field.Size / 2;

            for (var i = 0; i < Field.Size; i++) //to add word to start
                Field.Cells[center, i].Letter = initialWord[i];
        }
    }
}