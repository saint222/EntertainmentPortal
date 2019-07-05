namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///     Entity class, which specifies all properties of players stored in database.
    /// </summary>
    public class PlayerDb
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
        public GameDb Game { get; set; }

        /// <summary>
        ///     Player external key.
        /// </summary>
        public int? FirstPlayerId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>PlayerDb</c>.
        /// </remarks>
        public FirstPlayerDb FirstPlayer { get; set; }

        /// <summary>
        ///     Player external key.
        /// </summary>
        public int? SecondPlayerId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>PlayerDb</c>.
        /// </remarks>
        public SecondPlayerDb SecondPlayer { get; set; }
    }
}