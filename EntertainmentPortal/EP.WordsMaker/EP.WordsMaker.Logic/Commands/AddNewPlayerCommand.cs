using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
    public class AddNewPlayerCommand : IRequest<Result<Player>>
    {
        public string Name { get; set; }
    }
}