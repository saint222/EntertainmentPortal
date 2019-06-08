using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class ResetDeckCommand : IRequest<Deck>
    {
        public int Id { get; }
        public ResetDeckCommand(int id)
        {
            Id = id;
        }
    }
}
