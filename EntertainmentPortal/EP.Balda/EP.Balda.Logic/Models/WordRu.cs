using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    ///     Entity of WordsSource.
    /// </summary>
    public class WordRu
    {
        /// <summary>
        ///     Id property. Represents Id of Word.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Word property. Represents Word in WordsSource.
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        ///     PlayerWords property. Used for many-to-many relationships.
        /// </summary>
        public ICollection<PlayerWord> PlayerWords { get; set; }
    }
}