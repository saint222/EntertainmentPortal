using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class GetDeckQuery : IRequest<Result<Deck>>
    {
        private readonly string _email;
        public string Email
        {
            get { return _email; }
        }
        public GetDeckQuery(string email)
        {
            _email = email;
        }
    }
}
