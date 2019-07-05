using System;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
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
