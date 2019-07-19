using CSharpFunctionalExtensions;
using EP.Balda.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.Balda.Logic.Commands
{
    public class AddWordToPlayerCommand : IRequest<Result<Map>>
    {
        public string PlayerId { get; set; }

        public long GameId { get; set; }

        public List<Cell> CellsThatFormWord { get; set; }
    }
}