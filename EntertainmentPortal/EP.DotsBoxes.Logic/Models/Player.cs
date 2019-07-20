using System;

namespace EP.DotsBoxes.Logic.Models
{
    /// <summary>
    /// <c>Player</c> model class.
    /// Represents a Player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The class Player constructor.
        /// </summary>
        public Player()
        {
            Color = CreateRandomColor();
        }

        /// <summary>
        /// Id property. Represents unique player's Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name property. Represents player's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color property. Represents player's сolor.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Score property. Represents player's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// CreateRandomColor method. Аllows to randomly create a color for a player.
        /// </summary>
        private string CreateRandomColor()
        {
            Random randon = new Random();
            
            return System.Drawing.Color.FromArgb(randon.Next(255),
                randon.Next(255),
                randon.Next(255)).Name;
        }
    }
}
