using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Queries
{
    public class GetAllGamesQuery : IRequest<Maybe<IEnumerable<Game>>>
    {
    }
}
