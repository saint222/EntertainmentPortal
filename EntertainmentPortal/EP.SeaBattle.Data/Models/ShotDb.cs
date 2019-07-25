using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Data.Models
{
    public class ShotDb
    {
        public string Id { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public CellStatus Status { get; set; }
        public string PlayerId { get; set; }
        public PlayerDb Player { get; set; }
        public string GameId { get; set; }
        public GameDb Game { get; set; }
    }
}
