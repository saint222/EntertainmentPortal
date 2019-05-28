using EP.Sudoku.Logic.Enums;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>    
    /// Is used to represent an instance of the game.
    /// </summary>
    public class Session
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public DifficultyLevel Level { get; set; }
        public bool IsOver { get; set; }
        /// <summary>
        /// conserns players' ExperiencePoints 
        /// </summary>
        public double Duration { get; set; }
    }
}
