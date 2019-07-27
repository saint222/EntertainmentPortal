using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Commands
{
    public class FinishGameCommand : IRequest<Result<Game>>
    {
        public string UserId { get; set; }
    }
}
