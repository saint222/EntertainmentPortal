using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// <c>Game</c> model class.
    /// Represents the game process.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The field stores an Id of the map in the game.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// InitWord property. Represents initial word on the game map.
        /// </summary>
        public string InitWord { get; set; }

        /// <summary>
        /// MapId property. Represents Id of Map.
        /// </summary>
        public long MapId { get; set; }

        /// <summary>
        /// Map property. Navigation property of Map.
        /// </summary>
        public Map Map { get; set; }

        /// <summary>
        /// IsGameOver property. Represents boolean if game is over.
        /// </summary>
        public bool IsGameOver { get; set; }

        /// <summary>
        /// The field represents player's turn in the game.
        /// </summary>
        public string PlayerIdTurn { get; set; }

        /// <summary>
        /// The field represents players in the game.
        /// </summary>
        public List<Player> Players { get; set; }
    }
}