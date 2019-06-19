using System;

namespace EP.WordsMaker.Data.Models
{
    /// <summary>
    /// Represents <c>PlayerDb</c> class.
    /// </summary>
    public class PlayerDb
    {
        /// <summary>
        /// ID property
        /// </summary>
        /// <value>Represents unique id of player</value>
        public int Id { get; set; }

        /// <summary>
        /// Name property
        /// </summary>
        /// <value>Represents nickname of player</value>
        public string Name { get; set; }

        /// <summary>
        /// Score property
        /// </summary>
        /// <value>Represents score of player</value>
        public int Score { get; set; }

        public int BestScore { get; set; }

		public int BestScoreId { get; set; }

		/// <summary>
		/// LastGame property
		/// </summary>
		/// <value>Represents last game player</value>
		public DateTime LastGame { get; set; }
    }
}
