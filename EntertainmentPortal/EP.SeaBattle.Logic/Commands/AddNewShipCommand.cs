using CSharpFunctionalExtensions;
using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddNewShipCommand : IRequest<Maybe<IEnumerable<Ship>>>
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public ShipOrientation Orientation { get; set; }
        public ShipRank Rank { get; set; }

        //TODO Change to Guid
        public string PlayerId { get; set; }
        public Player Player { get; set; }
        public string GameId { get; set; }
        public Game Game { get; set; }
    }
}
