using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Commands
{
    public class GetPlayerQuery : IRequest<Result<Player>>
    {
        public string UserId { get; set; }
    }
}
