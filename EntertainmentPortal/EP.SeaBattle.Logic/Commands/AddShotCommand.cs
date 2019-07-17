using CSharpFunctionalExtensions;
using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddShotCommand : IRequest<Maybe<IEnumerable<Shot>>>
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public string PlayerId { get; set; }
        public string GameId { get; set; }
    }
}
