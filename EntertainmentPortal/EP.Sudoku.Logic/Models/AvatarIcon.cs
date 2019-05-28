using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Models
{
    /// <summary>
    /// The model <c>AvatarIcon</c> class.
    /// Represents an icon for a player's profile.
    /// </summary>
    public class AvatarIcon
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public bool IsBaseIcon { get; set; }
    }
}
