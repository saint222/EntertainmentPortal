using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Maps.
    /// </summary>
    public class MapDb
    {
        public long Id { get; private set; }

        public List<CellDb> Cells { get; set; }
    }
}
