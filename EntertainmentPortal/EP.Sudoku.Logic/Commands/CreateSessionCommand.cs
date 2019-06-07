using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class CreateSessionCommand : IRequest<Session>
    {
        public Player Participant { get; set; }

        public DifficultyLevel Level { get; set; }

        public int Hint { get; set; } = 3;

        public bool IsOver { get; set; }

        public double Duration { get; set; }

        public Cell Square { get; set; }
    }
}
