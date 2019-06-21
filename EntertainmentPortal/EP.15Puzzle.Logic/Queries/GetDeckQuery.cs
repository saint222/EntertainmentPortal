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
        private int _id;
        public int Id
        {
            get { return _id; }
        }
        public GetDeckQuery(int id)
        {
            _id = id;
        }
    }
}
