using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class MoveTile : IRequest<Deck>
    {
        private int _tile;
        private int _id;
        public int Tile
        {
            get { return _tile; }
        }
        public int Id
        {
            get { return _id; }
        }
        public MoveTile(int id, int tile)
        {
            _tile = tile;
            _id = id;
        }
    }
}
