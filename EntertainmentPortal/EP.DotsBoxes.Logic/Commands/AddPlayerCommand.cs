using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class AddPlayerCommand : IRequest<Result<Player>>
    {
        public string Name { get; set; }

        public string Color { get; set; }
    }
}
