// Filename: Players.cs
namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// The model <c>Players</c> class.
    /// Represents a Players.
    /// </summary>
    public class Player
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
    }
}
