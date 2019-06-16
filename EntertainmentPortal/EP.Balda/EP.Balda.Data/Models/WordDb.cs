using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Words.
    /// </summary>
    public class WordDb
    {
        public int Id { get; set; }
        
        public string Word { get; set; }

        public ICollection<PlayerWordDb> PlayerWords { get; set; }
    }
}