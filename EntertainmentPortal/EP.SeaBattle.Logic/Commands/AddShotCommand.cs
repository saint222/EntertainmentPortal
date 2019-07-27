using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using Newtonsoft.Json;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddShotCommand : IRequest<Result<Shot>>
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
