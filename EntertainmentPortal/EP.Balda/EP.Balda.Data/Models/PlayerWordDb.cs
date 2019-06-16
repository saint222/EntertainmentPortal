using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Table to form many-to-many relations.
    /// </summary>
    public class PlayerWordDb
    {
        public long PlayerId { get; set; }

        public PlayerDb Player { get; set; }

        public int WordId { get; set; }

        public WordDb Word { get; set; }
    }
}