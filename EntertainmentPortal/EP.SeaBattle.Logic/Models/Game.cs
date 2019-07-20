namespace EP.SeaBattle.Logic.Models
{
    public class Game
    {
        public string Id { get; set; }
        /// <summary>
        /// The game is looking for an enemy
        /// </summary>
        public bool EnemySearch { get; set; }
        /// <summary>
        /// Tells if the game is over
        /// </summary>
        public bool Finish { get; set; }
        public string Winner { get; set; }
        public string Loser { get; set; }
    }
}
