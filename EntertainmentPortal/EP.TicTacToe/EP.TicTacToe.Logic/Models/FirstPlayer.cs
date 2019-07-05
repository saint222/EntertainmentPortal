namespace EP.TicTacToe.Logic.Models
{
    public class FirstPlayer
    {
        public int Id { get; set; }

        /// <summary>
        ///     Char tic-tac-toe property. Represents char to store in the cell.
        /// </summary>
        /// <remarks>
        ///     <c>1</c> corresponds to a cross, <c>-1</c> zero, <c>0</c> empty field
        /// </remarks>
        public int TicTac { get; set; } = 1;

        /// <remarks>
        ///     Navigation property of <c>Player</c>.
        /// </remarks>
        public Player Player { get; set; }

        /// <summary>
        ///     Haunter external key.
        /// </summary>
        public string HaunterId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>FirstPlayer</c>.
        /// </remarks>
        public Haunter Haunter { get; set; }
    }
}