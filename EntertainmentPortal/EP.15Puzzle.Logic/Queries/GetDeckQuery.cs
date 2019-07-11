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
        private readonly string _sub;
        public string Sub
        {
            get { return _sub; }
        }
        public GetDeckQuery(string sub)
        {
            _sub = sub;
        }
    }
}
