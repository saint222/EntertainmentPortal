using System;
using System.Collections.Generic;
using System.Text;
using EP.WordsMaker.Logic.Models;
using MediatR;
namespace EP.WordsMaker.Logic.Queries
{
    public class GetAllPlayers : IRequest<IEnumerable<Player>>
    {
    }
}
