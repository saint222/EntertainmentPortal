using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Commands
{
    public class AddNewPlayerCommand : IRequest<Player>
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int Score { get; set; }
    }
}
