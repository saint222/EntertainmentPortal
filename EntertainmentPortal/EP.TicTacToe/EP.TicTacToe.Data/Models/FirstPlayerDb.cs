namespace EP.TicTacToe.Data.Models
{
    public class FirstPlayerDb
    {
        public int Id { get; set; }

        /// <remarks>
        ///     Navigation property of <c>PlayerDb</c>.
        /// </remarks>
        public PlayerDb Player { get; set; }

        /// <summary>
        ///     Char tic-tac-toe property. Represents char to store in the cell.
        /// </summary>
        /// <remarks>
        ///     <c>1</c> corresponds to a cross, <c>-1</c> zero, <c>0</c> empty field
        /// </remarks>
        public int TicTac { get; set; } = 1;

        /// <summary>
        ///     Haunter external key.
        /// </summary>
        public string HaunterId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>FirstPlayerDb</c>.
        /// </remarks>
        public HaunterDb Haunter { get; set; }
    }
}