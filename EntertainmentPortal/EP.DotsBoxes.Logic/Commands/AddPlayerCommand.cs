using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class AddPlayerCommand : IRequest<Result<Player>>
    {
        public int GameBoardId { get; set; }

        public string Name { get; set; }
    }
}
