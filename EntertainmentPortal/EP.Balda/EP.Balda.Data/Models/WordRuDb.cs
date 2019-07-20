using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    /// Entity of WordsSource.
    /// </summary>
    public class WordRuDb
    {
        /// <summary>
        /// Id property. Represents Id of Word.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Word property. Represents Word in WordsSource.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// PlayerWords property. Used for many-to-many relationships.
        /// </summary>
        public IList<PlayerWord> PlayerWords { get; set; }
    }
}