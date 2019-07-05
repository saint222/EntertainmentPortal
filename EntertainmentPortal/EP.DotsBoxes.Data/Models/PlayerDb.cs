using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EP.DotsBoxes.Data.Models
{
    /// <summary>
    /// <c>Player</c> model class.
    /// Represents a Player.
    /// </summary>
    public class PlayerDb
    {
        /// <summary>
        /// Id property. Stores unique player's Id.
        /// </summary>

        public int Id { get; set; }

        public int GameBoardId { get; set; }

        /// <summary>
        /// Name property. Stores player's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color property. Stores player's сolor.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Score property. Stores player's score.
        /// </summary>
        public int Score { get; set; }

        public virtual GameBoardDb GameBoard { get; set; }

        /// <summary>
        /// Created property. Stores date of creation / registration of the player.
        /// </summary>
        /// <seealso cref="System">
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
