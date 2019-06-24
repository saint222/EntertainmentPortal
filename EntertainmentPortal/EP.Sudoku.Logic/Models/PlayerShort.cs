using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent a player of the game (Short Version).
    /// </summary>
    public class PlayerShort
    {
        /// <summary>    
        /// Is used to denote an identification value of a player (Short Version).
        /// </summary>
        public long Id { get; set; }

        /// <summary>    
        /// Is used to denote the nickname of a player (Short Version).
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Is used to denote convertation of gamesessions duration (Short Version).
        /// </summary>
        public int ExperiencePoint { get; set; }

        /// <summary>
        /// Is used to denote player's progression feature due to his/her sum of experience points (Short Version).
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Is used to denote strict quantity of icons for choosing (Short Version).
        /// </summary>
        /// <remarks>
        /// Navigation property.
        /// </remarks> 
        public AvatarIcon Icon { get; set; }
    }   
    
}
