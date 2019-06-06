using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Queries
{
    public class UpdateCell : IRequest<Cell>
    {
        public Cell cell { get; set; }

        public UpdateCell(Cell c)
        {
            cell = c;
        }
    }
}
