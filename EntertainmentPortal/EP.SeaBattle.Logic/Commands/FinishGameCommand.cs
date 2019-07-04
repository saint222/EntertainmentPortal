using CSharpFunctionalExtensions;
using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Commands
{
    public class FinishGameCommand : IRequest<Result<Game>>
    {
        public string PlayerId { get; set; }
        public bool Finish { get; set; }
    }
}
