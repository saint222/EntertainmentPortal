using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Web.Models
{
    public class Message
    {
        private string _text;
        private Player _player;

        public Player Player { get => _player; set => _player = value; }

        public string Text { get => _text; set => _text = value; }
    }
}
