namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Class, which specifies all properties of players in a current game.
    /// </summary>
    public class Player
    {
        /// <summary>
        ///     Property indicates unique Id-number of a player.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Player external key.
        /// </summary>
        public int? GameId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>PlayerDb</c>. Used for one-to-one relationships.
        /// </remarks>
        public Game Game { get; set; }

        /// <summary>
        ///     Player external key.
        /// </summary>
        public int? FirstPlayerId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>PlayerDb</c>.
        /// </remarks>
        public FirstPlayer FirstPlayer { get; set; }

        /// <summary>
        ///     Player external key.
        /// </summary>
        public int? SecondPlayerId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>PlayerDb</c>.
        /// </remarks>
        public SecondPlayer SecondPlayer { get; set; }
    }
}