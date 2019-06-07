using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class CreateNewPlayerCommand : IRequest<Result<Player>>
    {
        public string NickName { get; set; }
        
        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}
