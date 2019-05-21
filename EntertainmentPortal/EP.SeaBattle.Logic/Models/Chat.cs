using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.SeaBattle.Logic.Models
{
    /// <summary>
    /// Ingame text chat
    /// </summary>
    public class Chat
    {
        private List<Message> _messages;

        public List<Message> Messages { get => _messages; set => _messages = value; }
    }
}
