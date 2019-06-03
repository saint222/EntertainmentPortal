using EP.Sudoku.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Queries
{
    public class GetPlayerById : IRequest<Player>
    {
        public int Id { get; set; }

        public GetPlayerById(int id)
        {
            Id = id;
        }
    }
}
