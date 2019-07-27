using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Newtonsoft.Json;

namespace EP.SeaBattle.Logic.Commands
{
    
    public class UpdatePlayerCommand : IRequest<Result<Player>>
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public string NickName { get; set; }
    }
}
