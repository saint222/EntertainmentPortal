using CSharpFunctionalExtensions;
using EP.SeaBattle.Common.Enums;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.SeaBattle.Logic.Commands
{
    public class DeleteShipCommand : IRequest<Maybe<IEnumerable<Ship>>>
    {
        //TODO Change to Guid
        public string Id { get; set; }
    }
}
