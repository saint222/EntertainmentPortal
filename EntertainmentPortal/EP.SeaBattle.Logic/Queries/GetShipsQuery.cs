using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Queries
{
    public class GetShipsQuery : IRequest<Maybe<IEnumerable<Ship>>>
    {
        //TODO Change to Guid
        public string GameId { get; set; }
        public string PlayerId { get; set; }
    }
}
