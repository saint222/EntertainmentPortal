using System;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class NewDeckCommand : IRequest <Result<Deck>>
    {
        private readonly string _userName;
        private string _email;

        public string UserName
        {
            get { return _userName; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public NewDeckCommand(string userName, string email)
        {
            _userName = userName;
            _email = email;
        }
    }
}
