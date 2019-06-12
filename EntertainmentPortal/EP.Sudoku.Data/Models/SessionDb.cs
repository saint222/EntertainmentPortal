using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Models
{
    public class SessionDb
    {

        /// <summary>    
        /// Is used to denote an identification value of a game session (DbInfo).
        /// </summary>
        public int Id { get; set; }

        /// <summary>    
        /// Is used to denote the level of a game session difficulty (is expected to be chosen by a player) (DbInfo).
        /// </summary>
        /// <remarks>
        /// Non-primitive type DifficultyLevel (enum) is used (DbInfo).
        /// </remarks>         
        public int Level { get; set; }

        /// <summary>    
        /// Is used to denote the number of tips (DbInfo).
        /// </summary>
        public int Hint { get; set; } = 3;

        /// <summary>    
        /// Is used as a flag for the business logic of the Session class (DbInfo).
        /// </summary>
        public bool IsOver { get; set; }

        /// <summary>
        /// Is used to denote a period of time from the beginning of a game session till it's end for subsequent convertation to player's points of experience (DbInfo). 
        /// </summary>        
        public double Duration { get; set; }

        /// <summary>    
        /// Is used to denote the necessary data, which concernes a player of a game session (DbInfo).
        /// </summary>
        /// <remarks>
        /// Navigation property.
        /// </remarks>  
        public int ParticipantDbId { get; set; }

        /// <summary>
        /// Is used for keeping a strict number (81) of the gameboard parts (DbInfo). 
        /// </summary> 
        public List<CellDb> SquaresDb { get; set; }
        
        
    }
}
