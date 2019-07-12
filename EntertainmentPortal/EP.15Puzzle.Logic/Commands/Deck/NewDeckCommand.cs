using System;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class NewDeckCommand : IRequest <Result<Deck>>
    {
        private string _sub;
        private readonly string _userName;
        private readonly string _email;

        public string Sub
        {
            get {return _sub;}
            set { _sub = value; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string Email
        {
            get { return _email; }
        }
        public NewDeckCommand(string sub, string userName, string email)
        {
            _sub = sub;
            _sub = sub;
            _userName = userName;
            _email = email;
        }
    }
}
