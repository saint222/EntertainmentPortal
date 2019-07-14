using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddShotCommand : IRequest<Result<Shot>>
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public string PlayerId { get; set; }
    }
}
