using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Commands
{
    public class AddLetterCommand : IRequest<Cell>
    {
        public long MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char? Letter { get; set; }
    }
}