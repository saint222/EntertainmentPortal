using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Web.Models
{
    public class GameManager
    {
        private Player _player1;
        private Player _player2;
        private bool[,] _shipFieldPlayer1;
        private bool[,] _turnFieldPlayer1;
        private bool[,] _shipFieldPlayer2;
        private bool[,] _turnFieldPlayer2;
        private Chat _chat;

        public Player Player1 { get => _player1; set => _player1 = value; }
        public Player Player2 { get => _player2; set => _player2 = value; }

        public bool[,] ShipFieldPlayer1 { get => _shipFieldPlayer1; set => _shipFieldPlayer1 = value; }

        public bool[,] TurnFieldPlayer1 { get => _turnFieldPlayer1; set => _turnFieldPlayer1 = value; }

        public bool[,] ShipFieldPlayer2 { get => _shipFieldPlayer2; set => _shipFieldPlayer2 = value; }

        public bool[,] TurnFieldPlayer2 { get => _turnFieldPlayer2; set => _turnFieldPlayer2 = value; }

        public Chat Chat { get => _chat; set => _chat = value; }

    }
}
