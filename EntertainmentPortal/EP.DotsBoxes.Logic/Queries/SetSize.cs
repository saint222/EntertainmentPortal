using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class SetSize : IRequest<GameBoard>, IRequest<int[,]>
    {
        public SetSize(string [] array)
        {
            NewRow = Convert.ToInt32(array[0]);
            NewColumn = Convert.ToInt32(array[1]);
          
        }

        public int NewRow { get; set; }
        public int NewColumn { get; set; }

    }
}
