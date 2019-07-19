using EP.Balda.Logic.Models;
using System.Collections.Generic;

namespace EP.Balda.Web.Models
{
    /// <summary>
    /// Represents the game and cells that forms a word.
    /// </summary>
    public class GameAndCells
    {
        /// <summary>
        /// GameId property. Represents Id of Game.
        /// </summary>
        public long GameId { get; set; }

        /// <summary>
        /// Property for cells' id that form a word.
        /// </summary>
        public List<Cell> CellsIdFormWord { get; set; }
    }
}
