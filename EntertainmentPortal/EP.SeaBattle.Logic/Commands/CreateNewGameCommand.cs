using CSharpFunctionalExtensions;
using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Commands
{
    public class CreateNewGameCommand : IRequest<Result<Game>>
    {
        public string Player1Id { get; set; }
        public Player Player1 { get; set; }
        public string Player2Id { get; set; }
        public Player Player2 { get; set; }
        public bool Finish { get; set; }
        public Player PlayerAllowedToMove { get; set; }
    }
}
