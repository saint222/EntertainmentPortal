using System;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class NewDeckCommand : IRequest <Result<Deck>>
    {
        private readonly string _sub;
        public string Sub
        {
            get { return _sub; }
        }
        public NewDeckCommand(string sub)
        {
            _sub = sub;
        }
    }
}
