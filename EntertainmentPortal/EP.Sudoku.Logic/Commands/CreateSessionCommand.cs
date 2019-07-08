using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class CreateSessionCommand : IRequest<Result<Session>>
    {

        public DifficultyLevel Level { get; set; }

        public int Hint { get; set; } = 3;

        public bool IsOver { get; set; }

        public long PlayerId { get; set; }
    }
}
