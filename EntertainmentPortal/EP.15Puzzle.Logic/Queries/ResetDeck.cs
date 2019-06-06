using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class ResetDeck : IRequest<Deck>
    {
        public int Id { get; }
        public ResetDeck(int id)
        {
            Id = id;
        }
    }
}
