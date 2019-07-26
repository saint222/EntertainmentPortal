using EP.Balda.Logic.Models;

namespace EP.Balda.Web.Models
{
    /// <summary>
    /// Represents the game status and map cells.
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
        /// IsGameOver property. Represents boolean if game is over.
        /// </summary>
        public bool IsGameOver { get; set; }

        /// <summary>
        /// Property for cells of the map.
        /// </summary>
        public Cell[,] Cells { get; set; }
    }
}
