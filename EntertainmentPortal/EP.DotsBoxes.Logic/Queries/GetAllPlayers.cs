using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetAllPlayers : IRequest<Maybe<IEnumerable<Player>>>
    {

    }
}
