using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetGameBoard : IRequest<IEnumerable<GameBoard>>
    {
    }
}
