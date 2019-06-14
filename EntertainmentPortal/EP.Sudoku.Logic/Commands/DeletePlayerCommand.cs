using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Commands
{
    public class DeletePlayerCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeletePlayerCommand(int id)
        {
            Id = id;
        }
    }
}
