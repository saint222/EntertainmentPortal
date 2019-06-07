using EP.Sudoku.Logic.Enums;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent an instance of the game.
    /// </summary>
    public class Session
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
        public Player Participant { get; set; }

        /// <summary>    
        /// Is used to denote the level of a game session difficulty (is expected to be chosen by a player).
        /// </summary>
        /// <remarks>
        /// Non-primitive type DifficultyLevel (enum) is used.
        /// </remarks> 
        public DifficultyLevel Level { get; set; }

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

        /// <summary>
        /// Is used to connection with the gameboard. 
        /// </summary> 
        public Cell Square { get; set; }
    }
}
