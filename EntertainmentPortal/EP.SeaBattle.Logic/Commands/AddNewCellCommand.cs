using CSharpFunctionalExtensions;
using MediatR;
using EP.SeaBattle.Logic.Models;
using EP.SeaBattle.Common.Enums;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddNewCellCommand : IRequest<Result<Cell>>
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public CellStatus Status { get; set; }
    }
}
