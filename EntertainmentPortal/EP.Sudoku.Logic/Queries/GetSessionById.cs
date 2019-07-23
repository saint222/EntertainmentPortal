using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Queries
{
    public class GetSessionById : IRequest<Maybe<Session>>
    {
        public long Id { get; set; }
    }
}
