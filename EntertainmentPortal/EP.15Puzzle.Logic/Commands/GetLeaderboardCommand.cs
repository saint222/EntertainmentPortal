using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class GetLeaderboardCommand : IRequest <Result<IEnumerable<Record>>>
    {
        public GetLeaderboardCommand()
        {
        }
    }
}
