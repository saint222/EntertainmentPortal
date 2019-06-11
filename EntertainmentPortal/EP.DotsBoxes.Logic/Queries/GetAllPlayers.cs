using System;
using System.Collections.Generic;
using System.Text;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetAllPlayers : IRequest<IEnumerable<Player>>
    {
    }
}
