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
        ///     Cells property. Navigation property of CellDb.
        /// </summary>
        public List<CellDb> Cells { get; set; }

        /// <summary>
        ///     Game external key.
        /// </summary>
        public int GameId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>GameDb</c>.
        /// </remarks>
        public GameDb Game { get; set; }


        /// <summary>
        ///     Value of a continuous line of a chain of moves.
        /// </summary>
        public int ChainLength { get; set; }


        /// <summary>
        ///     Chain external key.
        /// </summary>
        public string PlayerId { get; set; }

        /// <remarks>
        ///     Navigation property of set <c>ChainDb</c>.
        /// </remarks>
        public PlayerDb Player { get; set; }
    }
}