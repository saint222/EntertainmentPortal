using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.Balda.Logic.Commands
{
    public class AddWordToPlayerCommand : IRequest<Result<Player>>
    {
        public long Id { get; set; }

        public long GameId { get; set; }

        public List<long> CellsIdFormWord { get; set; }
    }
}