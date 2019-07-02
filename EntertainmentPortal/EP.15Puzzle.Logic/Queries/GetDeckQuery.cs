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
        public GetDeckQuery(string authType, string authId)
        {
            _authType = authType;
            _authId = authId;
        }
    }
}
