using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class DeletePlayerCommand : IRequest<Result<Player>>
    {
        public long Id { get; set; }
    }
}