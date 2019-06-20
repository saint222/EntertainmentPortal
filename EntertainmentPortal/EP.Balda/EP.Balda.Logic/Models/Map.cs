using System;
using System.Collections.Generic;
using EP.Balda.Logic.Interfaces;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     <c>Map</c> model class.
    ///     Represents the game map.
    /// </summary>
    public class Map
    {
        /// <summary>
        ///     Id property. Represents playing field Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        ///     Fields property. Represents playing field as array of cells.
        /// </summary>
        public List<Cell> Cells { get; set; }
    }
}