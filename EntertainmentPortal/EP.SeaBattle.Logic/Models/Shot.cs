using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Logic.Models
{
    public class Shot
    {
        public Shot(string playerId, string gameId, byte x, byte y, CellStatus status)
        {
            PlayerId = playerId;
            GameId = gameId;
            X = x;
            Y = y;
            Status = status;
        }

        public Shot()
        {

        }

        public byte X { get; set; }
        public byte Y { get; set; }
        public CellStatus Status { get; set; }
        public string PlayerId { get; set; }
        public string GameId { get; set; }
    }
}
