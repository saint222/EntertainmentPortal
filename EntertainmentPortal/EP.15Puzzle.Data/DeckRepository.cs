using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EP._15Puzzle.Data.Models;

namespace EP._15Puzzle.Data
{
    public static class DeckRepository
    {
        private static Dictionary<int, DeckDB> _decks = new Dictionary<int, DeckDB>()
        {
            {1,new DeckDB() }
        };
        public static DeckDB Get(int id)
        {
            if (_decks.ContainsKey(id))
            {
                return _decks[id];
            }
            return null;
        }

        public static DeckDB Create(int id)
        {
            _decks.Add(id,new DeckDB());
            return Get(id);
        }

        public static DeckDB Update(int id, IEnumerable<int> tiles, int score, bool victory)
        {
            if (_decks.ContainsKey(id))
            {
                _decks[id].Tiles=(List<int>)tiles;
            }
            return Get(id);
        }
        public static bool Delete(int id)
        {
            if (_decks.ContainsKey(id))
            {
                _decks.Remove(id);
                return true;
            }
            return false;
        }
    }

}
