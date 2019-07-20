using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Logic.Models
{
    public class Shot
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public ShotStatus Status { get; set; }
    }
}
