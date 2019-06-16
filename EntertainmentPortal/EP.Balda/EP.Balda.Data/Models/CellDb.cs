namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Cells.
    /// </summary>
    public class CellDb
    {
        public long Id { get; set; }

        public MapDb Map { get; set; }

        public long MapId { get; set; }

        public int X { get; }

        public int Y { get; }

        public char? Letter { get; set; }
    }
}