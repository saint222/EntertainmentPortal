using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class SetSize : IRequest<GameBoard>, IRequest<int[,]>
    {
        public SetSize(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
          
        }

        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
