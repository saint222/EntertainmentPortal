using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// <c>Map</c> model class.
    /// Represents the game map.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Id property. Represents playing field Id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Size property. Represents size of playing field.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Fields property. Represents playing field as array of cells.
        /// </summary>
        public List<Cell> Cells { get; set; }
    }
}