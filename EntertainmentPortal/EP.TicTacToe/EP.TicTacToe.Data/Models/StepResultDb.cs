namespace EP.TicTacToe.Data.Models
{
    /// <summary>
    ///    The state of the game.
    /// </summary>
    public class StepResultDb
    {
        /// <summary>
        ///     Id of result step.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The ID of the next player.
        /// </summary>
        public int NextPlayerId { get; set; }

        /// <summary>
        ///     Indicator of the current status of the game
        /// </summary>
        public State Status { get; set; } = State.Continuation;

        /// <summary>
        ///     Game external key.
        /// </summary>
        public int GameId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>GameDb</c>. Used for one-to-many relationships.
        /// </remarks>
        public GameDb Game { get; set; }
    }

    /// <summary>
    ///     Enum of game states.
    /// </summary>
    public enum State
    {
        Continuation,
        Winning,
        Draw
    }
}