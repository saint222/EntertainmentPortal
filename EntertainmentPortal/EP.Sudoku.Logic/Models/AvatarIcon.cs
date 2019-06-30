using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent an icon for a player's profile.
    /// </summary>
    public class AvatarIcon
    {
        /// <summary>    
        /// Is used to denote an identification value of a player's avatar picture.
        /// </summary>
        public long Id { get; set; }

        /// <summary>    
        /// Is used to denote URI of a player's avatar picture.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>    
        /// Is used as a flag for setting up the default value of player's avatar picture.
        /// </summary>
        public bool IsBaseIcon { get; set; }
    }
}
