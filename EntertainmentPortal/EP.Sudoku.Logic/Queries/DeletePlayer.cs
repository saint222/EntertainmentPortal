using EP.Sudoku.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Queries
{
    public class DeletePlayer : IRequest<bool>
    {
        public int Id { get; set; }
        
        public DeletePlayer(int id)
        {
            Id = id;
        }
    }
}
