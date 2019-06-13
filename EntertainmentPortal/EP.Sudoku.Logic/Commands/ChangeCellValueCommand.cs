using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class ChangeCellValueCommand : IRequest<Result<Session>>
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }
    }
}
