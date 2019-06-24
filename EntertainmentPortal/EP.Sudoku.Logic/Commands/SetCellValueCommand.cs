using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class SetCellValueCommand : IRequest<Result<Session>>
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int SessionId { get; set; }
    }
}
