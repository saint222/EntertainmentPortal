using System;
namespace EP.DotsBoxes.Data.Models
{
    /// <summary>
    /// The model <c>PlayerDb</c> class.
    /// Represents a Players.
    /// </summary>
    public class PlayerDb
    {
        /// <summary>
        /// Id property.
        /// </summary>
        /// <value>
        /// A value represents unique player's Id.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Name property.
        /// </summary>
        /// <value>
        /// A value represents player's nickname.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Color property.
        /// </summary>
        /// <value>
        /// A value represents player's сolor.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Score property.
        /// </summary>
        /// <value>
        /// A value represents player's score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        /// Created property.
        /// </summary>
        /// <value>
        /// A value represents date of creation / registration of the player.
        /// </value>
        /// <seealso cref="System">
        public DateTime Created { get; set; }

    }

}
