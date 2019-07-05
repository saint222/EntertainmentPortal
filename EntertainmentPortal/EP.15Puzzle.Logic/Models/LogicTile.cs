using System.Collections.Generic;

namespace EP._15Puzzle.Logic.Models
{
    public class LogicTile
    {
        public int Id { get; set; }
        public int Pos { get; set; }
        public int Num { get; set; }
        public ICollection<LogicTile> NearbyTiles { get; set; } = new List<LogicTile>();

        public LogicTile(int num, int size)
        {
            Num = num;
            if (num==0)
            {
                Pos = size*size;
            }
            else
            {
                Pos = num;
            }
        }

        public LogicTile()
        {
        }
    }
}
