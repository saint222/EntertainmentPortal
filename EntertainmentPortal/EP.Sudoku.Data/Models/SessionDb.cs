using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Models
{
    public class SessionDb
    {
        /// <summary>    
        /// Is used to denote an identification value of a game session.
        /// </summary>
        public int Id { get; set; }

        /// <summary>    
        /// Is used to denote the necessary data, which concernes a player of a game session.
        /// </summary>
        /// <remarks>
        /// Non-primitive type Player is used.
        /// </remarks> 
        public PlayerDb ParticipantDb { get; set; }

        /// <summary>    
        /// Is used to denote the level of a game session difficulty (is expected to be chosen by a player).
        /// </summary>
        /// <remarks>
        /// Non-primitive type DifficultyLevel (enum) is used.
        /// </remarks>
        ///
        /// 
        public int Level { get; set; } // как быть с enum?

        /// <summary>    
        /// Is used to denote the number of tips.
        /// </summary>
        public int Hint { get; set; } = 3;

        /// <summary>    
        /// Is used as a flag for the business logic of the Session class.
        /// </summary>
        public bool IsOver { get; set; }

        /// <summary>
        /// Is used to denote a period of time from the beginning of a game session till it's end for subsequent convertation to player's points of experience. 
        /// </summary>        
        public double Duration { get; set; }

        //
        //
        //
        //public GameBoardDb GameDb { get; set; } тут тоже как быть? не хочется GameBoardDb создавать
    }
}
