using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Commands
{
    public class GetPlayerCommand : IRequest<Result<Player>>
    {
        public string Id { get; set; }
    }
}
