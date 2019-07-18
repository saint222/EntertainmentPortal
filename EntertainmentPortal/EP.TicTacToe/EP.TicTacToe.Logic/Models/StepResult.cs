namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///    The state of the game.
    /// </summary>
    public class StepResult
    {
        /// <summary>
        ///     ID Game.
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        ///     The ID of the next player.
        /// </summary>
        public int NextPlayerId { get; set; }

        /// <summary>
        ///     Indicator of the current status of the game
        /// </summary>
        public State Status { get; set; } = State.Continuation;
    }

    /// <summary>
    ///     Enum of game states.
    /// </summary>
    public enum State
    {
        Continuation,
        Winning,
        Draw,
        Loss
    }
}