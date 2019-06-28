namespace EP.TicTacToe.Data.Models
{
    public class StepDb
    {
        /// <summary>
        ///     Id property. Represents Id of StepDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     CellDb property. Navigation property of CellDb.
        /// </summary>
        public CellDb Cell { get; set; }

        /// <summary>
        ///     GameDb property. Navigation property of GameDb.
        /// </summary>
        public GameDb Game { get; set; }

        /// <summary>
        ///     PlayerDb property. Navigation property of StepDb.
        ///     Used for one-to-many relationships.
        /// </summary>
        public int PlayerId { get; set; }
        public PlayerDb Player { get; set; }
    }
}