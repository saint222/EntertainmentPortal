using System.Collections.Generic;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetAllPlayers : IRequest<IEnumerable<Player>>
    {
    }
}