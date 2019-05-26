using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class GetDeck : IRequest<Deck>
    {
        private int _id;
        public int Id
        {
            get { return _id; }
        }
        public GetDeck(int id)
        {
            _id = id;
        }
    }
}
