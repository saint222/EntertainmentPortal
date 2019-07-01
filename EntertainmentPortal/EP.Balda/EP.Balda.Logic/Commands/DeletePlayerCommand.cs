using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class DeletePlayerCommand : IRequest<Result<Player>>
    {
        public string Id { get; set; }
    }
}