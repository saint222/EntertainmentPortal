using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class UpdateGameBoardCommand : IRequest<Result<Cell>>
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public bool Top { get; set; }

        public bool Bottom { get; set; }

        public bool Left { get; set; }

        public bool Right { get; set; }
    }
}

