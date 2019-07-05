using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using System.Collections.Generic;
using MediatR;


namespace EP.SeaBattle.Logic.Queries
{
    public class GetAllPlayersQuery : IRequest<Maybe<IEnumerable<Player>>>
    {}
}
