using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class UpdateCellCommand : IRequest<Cell>
    {
        public Cell cell { get; set; }

        public UpdateCellCommand(Cell cell)
        {
            this.cell = cell;
        }
    }
}
