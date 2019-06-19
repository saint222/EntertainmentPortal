using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class NewDeck : IRequest<Deck>
    {
        public int Id { get; }
        public NewDeck(int id)
        {
            Id = id;
        }
    }
}
