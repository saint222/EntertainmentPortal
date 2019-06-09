using System;
using CSharpFunctionalExtensions;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class NewDeckCommand : IRequest <Tuple<Result<Deck>, string>>
    {
        public NewDeckCommand()
        {
        }
    }
}
