using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.Balda.Logic.Queries
{
    public class GetAllPlayers : IRequest<Maybe<IEnumerable<Player>>>
    {
    }
}