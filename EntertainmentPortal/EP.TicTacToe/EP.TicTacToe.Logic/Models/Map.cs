namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Entity of Maps.
    /// </summary>
    public class Map
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

        /// <summary>
        ///     Game external key.
        /// </summary>
        public int? GameId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>Game</c>.
        /// </remarks>
        public Game Game { get; set; }

        /// <summary>
        ///     <c>Cell</c> navigation property. Used for one-to-one relationships.
        /// </summary>
        public Cell Cell { get; set; }
    }
}