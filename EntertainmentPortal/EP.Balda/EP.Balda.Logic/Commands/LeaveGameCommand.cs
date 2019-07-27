using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class LeaveGameCommand : IRequest<Result<Game>>
    {
        public long GameId { get; set; }
    }
}
