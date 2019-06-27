namespace EP.TicTacToe.Data.Models
{
    public class StepDb
    {
        /// <summary>
        ///     Id property. Represents Id of StepDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     PlayerId property. Represents Id of PlayerDb.
        /// </summary>
        public PlayerDb PlayerDb { get; set; }

        /// <summary>
        ///     CellDb property. Navigation property of CellDb.
        /// </summary>
        public CellDb CellDb { get; set; }

        /// <summary>
        ///     GameDb property. Navigation property of GameDb.
        /// </summary>
        public GameDb GameDb { get; set; }
    }
}