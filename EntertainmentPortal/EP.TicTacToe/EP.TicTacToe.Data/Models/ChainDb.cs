using System.Collections.Generic;

namespace EP.TicTacToe.Data.Models
{
    public class ChainDb
    {
        /// <summary>
        ///     Id property. Represents Id of ChainDb.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     StepDb property. Navigation property of StepDb.
        /// </summary>
        public List<StepDb> Steps { get; set; }

        /// <summary>
        ///     GameDb property. Navigation property of GameDb.
        /// </summary>
        public int GameId { get; set; }

        public GameDb Game { get; set; }


        /// <summary>
        ///     PlayerIb property. Navigation property of set <c>Players</c> in <c>Games</c>.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        ///     Value of a continuous line of a chain of moves.
        /// </summary>
        public int ChainLength { get; set; }
    }
}