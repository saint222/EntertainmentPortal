using System.Collections.Generic;

namespace EP.Balda.Data.Models
{
    /// <summary>
    ///     Entity of Games.
    /// </summary>
    public class GameDb
    {
        public long Id { get; set; }
        
        public MapDb Map { get; set; }

        public long MapId { get; set; }

        public ICollection<PlayerGameDb> PlayerGames { get; set; }
    }
}