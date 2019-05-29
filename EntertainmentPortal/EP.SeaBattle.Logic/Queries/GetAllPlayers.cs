using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Queries
{
    public class GetAllPlayers : IRequest<IEnumerable<Player>>
    {

    }
}
