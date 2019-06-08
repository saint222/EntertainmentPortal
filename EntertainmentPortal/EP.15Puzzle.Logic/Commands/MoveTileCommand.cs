using CSharpFunctionalExtensions;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class MoveTileCommand : IRequest <Result<Deck>>
    {
        public int Id { get; set; }
        public int Tile { get; set; }
        
    }
}
