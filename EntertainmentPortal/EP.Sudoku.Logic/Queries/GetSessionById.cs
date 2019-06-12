using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Queries
{
    public class GetSessionById : IRequest<Session>
    {
        public int Id { get; set; }

        public GetSessionById(int id)
        {
            Id = id;
        }
    }
}
