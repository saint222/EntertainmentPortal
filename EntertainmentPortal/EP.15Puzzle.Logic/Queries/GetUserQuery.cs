using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Queries
{
    public class GetUserQuery : IRequest<Result<User>>
    {
        private int _id;
        public int Id
        {
            get { return _id; }
        }
        public GetUserQuery(int id)
        {
            _id = id;
        }
    }
}
