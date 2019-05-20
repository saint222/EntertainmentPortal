using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP._15Puzzle.Web.Models
{
    public class Tile
    {
        private int _num;
        private int _posX;
        private int _posY;
        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }

        public int PosX
        {
            get { return _posX;}

        }
        public int PosY
        {
            get { return _posY; }

        }
        public Tile(int num, int posX,int posY)
        {
            _num = num;
            _posX = posX;
            _posY = posY;
        }
    }
    
}
