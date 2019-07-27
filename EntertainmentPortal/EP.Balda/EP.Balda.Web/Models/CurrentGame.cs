using EP.Balda.Logic.Models;

namespace EP.Balda.Web.Models
{
    /// <summary>
    /// Represents the game status.
    /// </summary>
    public class CurrentGame
    {
        /// <summary>
        /// The field stores an Id of the player in the current game.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The field stores an Id of the current game.
        /// </summary>
        public long GameId { get; set; }

        /// <summary>
        /// MapId property. Represents Id of Map.
        /// </summary>
        public long MapId { get; set; }

        /// <summary>
        /// IsGameOver property. Represents boolean if game is over.
        /// </summary>
        public bool IsGameOver { get; set; }

        /// <summary>
        /// The field represents if it's player's turn in the game.
        /// </summary>
        public bool IsPlayersTurn { get; set; }

        /// <summary>
        /// Score property. Represents player's Score.
        /// </summary>
        public int PlayerScore { get; set; }

        /// <summary>
        /// Score property. Represents player's opponent Score.
        /// </summary>
        public int OpponentScore { get; set; }

        /// <summary>
        /// Property for cells of the map.
        /// </summary>
        public Cell[,] Cells { get; set; }
    }
}
