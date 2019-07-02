using System.Collections.Generic;

namespace EP.TicTacToe.Logic.Models
{
    /// <summary>
    ///     Entity of Chains.
    /// </summary>
    public class Chain
    {
        /// <summary>
        ///     <c>Id</c> property. Represents <c>Id</c> of <c>Chain</c>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Cells property. Navigation property of <c>Cell</c>.
        /// </summary>
        public List<Cell> Cells { get; set; }

        /// <summary>
        ///     Game external key.
        /// </summary>
        public int GameId { get; set; }

        /// <remarks>
        ///     Navigation property of <c>Game</c>.
        /// </remarks>
        public Game Game { get; set; }
        
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
        public Player Player { get; set; }
    }
}