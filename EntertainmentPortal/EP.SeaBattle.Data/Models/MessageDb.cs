using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Data.Models
{
    public class MessageDb
    {
        private string _text;
        private PlayerDb _player;
        private Guid _gameId;
        private DateTime _date;

        public PlayerDb Player { get => _player; set => _player = value; }

        public string Text { get => _text; set => _text = value; }

        public Guid GameId { get => _gameId; set => _gameId = value; }

        public DateTime Date { get => _date; set => _date = value; }
    }
}
