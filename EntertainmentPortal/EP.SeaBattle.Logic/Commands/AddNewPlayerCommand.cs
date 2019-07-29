using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Newtonsoft.Json;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddNewPlayerCommand : IRequest<Result<Player>>
    {
        public string NickName { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
