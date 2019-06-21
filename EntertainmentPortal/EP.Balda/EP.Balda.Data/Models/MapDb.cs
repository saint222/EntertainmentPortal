using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Maps.
    /// </summary>
    public class MapDb
    {
        /// <summary>
        ///     Id property. Represents Id of Game.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Cells property. Represents Cells of map.
        /// </summary>
        public IList<CellDb> Cells { get; set; }
    }
}