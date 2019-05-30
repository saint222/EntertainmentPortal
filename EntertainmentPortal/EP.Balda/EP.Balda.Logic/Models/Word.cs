using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     The model <c>Word</c> class.
    ///     Represents aggregation of cells which contains the word entirely.
    /// </summary>
    public class Word
    {
        /// <summary>
        ///     Cells property. Represents the list of cells that contains letters of choosen word.
        /// </summary>
        public List<Cell> Cells { get; set; }
    }
}