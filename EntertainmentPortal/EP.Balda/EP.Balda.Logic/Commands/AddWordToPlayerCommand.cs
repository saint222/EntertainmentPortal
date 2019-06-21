using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class AddWordToPlayerCommand : IRequest<Result<Player>>
    {
        public long Id { get; set; }
        
        public long GameId { get; set; }

        public string Word { get; set; }
    }
}