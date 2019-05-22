using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
    public class Game
    {
        //Time _time;  
        
        public string KeyWord { get; set; }

        public Rules _rules;

        public Dictionary<Word, Player> _words;

        public List<Player> _players;

    }
}
