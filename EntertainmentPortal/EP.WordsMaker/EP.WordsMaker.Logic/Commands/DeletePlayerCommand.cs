using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
    public class DeletePlayerCommand: IRequest<Result<Player>>
    {
        public int Id { get; set;}
    }
}