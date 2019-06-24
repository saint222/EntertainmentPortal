using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent a player of the game.
    /// </summary>
    public class Player
    {
        /// <summary>    
        /// Is used to denote an identification value of a player.
        /// </summary>
        public long Id { get; set; }

        /// <summary>    
        /// Is used to denote the nickname of a player.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Is used to denote convertation of gamesessions duration.
        /// </summary>
        public int ExperiencePoint { get; set; }

        /// <summary>
        /// Is used to denote player's progression feature due to his/her sum of experience points.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Is used to denote strict quantity of icons for choosing.
        /// </summary>
        /// <remarks>
        /// Navigation property.
        /// </remarks> 
        public AvatarIcon Icon { get; set; }

        /// <summary>
        /// Is used to provide a player with a possibility to have more than one unfinished game.
        /// </summary> 
        public List<Session> GameSessions { get; set; }
    }
}
