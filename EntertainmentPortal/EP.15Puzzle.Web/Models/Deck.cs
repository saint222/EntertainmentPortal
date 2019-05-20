using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace EP._15Puzzle.Web.Models
{
    public class Deck
    {
        private readonly int _size;
        private List<Tile> _deck;
        public List<Tile> GetTiles
        {
            get { return _deck; }
        }
        public Deck(int size)
        {
            _size = size;
            int number = size * size;
            _deck = new List<Tile>();
            //add Tiles
            for (int i = 0; i < number-1; i++)
            {
                _deck.Add(new Tile(i+1,i/size+1,i%size+1));
            }
            Random random = new Random();
            //unsort
            for (int i = _deck.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                var temp = _deck[i].Num;
                _deck[i].Num = _deck[j].Num;
                _deck[j].Num = temp;
            }

            //add empty Tile
            _deck.Add(new Tile(0, size,size));
        }

        public List<Tile> Move(int num)
        {
            Tile tile = _deck.First(t => t.Num == num);
            Tile tile0 = _deck.First(t => t.Num == 0);
            if (PosCompare(tile,tile0))
            {
                int temp = tile.Num;
                tile.Num = tile0.Num;
                tile0.Num = temp;
            }

            return _deck;
        }


        private bool PosCompare(Tile tile, Tile tile0)
        {
            if (tile.PosX==tile0.PosX || tile.PosY == tile0.PosY)
            {
                if (Math.Abs(tile.PosX - tile0.PosX)==1 || Math.Abs(tile.PosY - tile0.PosY) == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckWin()
        {
            if (_deck[_deck.Count-1].Num==0)
            {
                for (int i = 1; i < _deck.Count; i++)
                {
                    if (_deck[i].Num!=i)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public string ToHTML()
        {
            int i = 0;
            StringBuilder str = new StringBuilder();
            for (int posX = 0; posX < _size; posX++)
            {
                str.Append("<p>| ");
                for (int posY = 0; posY < _size; posY++)
                {
                    str.Append(_deck[i].Num).Append(" |");
                    i++;
                }
                str.Append("</p>");
            }

            return str.ToString();
        }
    }
}
