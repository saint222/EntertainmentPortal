using System;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class NewDeckCommand : IRequest <Result<Deck>>
    {
        private string _authType;
        private string _authId;
        public string AuthId
        {
            get { return _authId; }
        }
        public string AuthType
        {
            get { return _authType; }
        }
        public NewDeckCommand(string authType, string authId)
        {
            _authType = authType;
            _authId = authId;
        }
    }
}
