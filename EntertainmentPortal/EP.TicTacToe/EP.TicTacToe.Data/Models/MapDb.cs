namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity of Maps.
    /// </summary>
    public class MapDb
    {
        /// <summary>
        ///     Id property. Represents Id of GameDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Size property. Represents size of playing field.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///     The size of the chain of characters to win on the card.
        /// </summary>
        public int WinningChain { get; set; }
    }
}