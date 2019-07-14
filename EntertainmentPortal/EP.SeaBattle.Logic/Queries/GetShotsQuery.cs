using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using System.Collections.Generic;
using MediatR;

namespace EP.SeaBattle.Logic.Queries
{
    public class GetShotsQuery : IRequest<Maybe<IEnumerable<Shot>>>
    {
        public string PlayerId { get; set; }
    }
}
