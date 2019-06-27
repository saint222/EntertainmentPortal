namespace EP.TicTacToe.Logic.Models
{
    public class Game
    {
        /// <summary>
        ///     Indicates lenth and width of the board.
        /// </summary>
        public const int SIZE = 3;

        /// <summary>
        ///     Array indicates cells of the game board.
        /// </summary>
        public static int[] field;

        /// <summary>
        ///     Property indicates unique Id-number of a game session.
        /// </summary>
        public uint Id { get; set; }
    }
}