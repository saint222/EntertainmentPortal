using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Data.Models
{
    public class PlayerDb
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public int ExperiencePoint { get; set; }
        public int Level { get; set; }
        public virtual AvatarIconDb AvatarIconDb { get; set; }
    }
}
