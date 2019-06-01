using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class CreateNewGameBoardCommand : IRequest<GameBoard>
    {
        public int Rows { get; set; }

        public int Columns { get; set; }
    }
}
