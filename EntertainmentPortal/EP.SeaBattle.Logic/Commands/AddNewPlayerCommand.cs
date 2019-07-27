using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddNewPlayerCommand : IRequest<Result<Player>>
    {
        public string NickName { get; set; }
        public string UserId { get; set; }
    }
}
