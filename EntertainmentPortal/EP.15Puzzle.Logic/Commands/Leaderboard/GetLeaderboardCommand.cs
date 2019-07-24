using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class GetLeaderboardCommand : IRequest<Result<IEnumerable<Record>>>
    {
        private readonly string _email;
        public string Email
        {
            get { return _email; }
        }
        public GetLeaderboardCommand(string email)
        {
            _email = email;
        }
    }
}
    

