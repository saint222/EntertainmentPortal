// Filename: Word.cs
using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// The model <c>Word</c> class.
    /// Represents aggregation of cells which contains the word entirely.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Cells property.
        /// </summary>
        /// <value>
        /// A value represents the list of cells that contains letters of choosen word.
        /// </value>
        /// <seealso cref="Cell"/>
        public List<Cell> Cells { get; set; } //cells that form words
    }
}