using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class NewDeckCommand : IRequest<Deck>
    {
        public NewDeckCommand()
        {
        }
    }
}
