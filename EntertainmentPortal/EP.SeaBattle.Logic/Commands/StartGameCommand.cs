using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Commands
{
    public class StartGameCommand : IRequest<Result<Game>>
    {
        public string PlayerId { get; set; }

    }
}
