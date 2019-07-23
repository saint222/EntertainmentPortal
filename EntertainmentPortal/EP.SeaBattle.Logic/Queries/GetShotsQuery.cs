using CSharpFunctionalExtensions;
using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Queries
{
    public class GetShotsQuery : IRequest<Maybe<IEnumerable<Shot>>>
    {
        public string GameId { get; set; }
        public string AnsweredPlayerID { get; set; }
    }
}
