using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class MoveTileCommand : IRequest <Result<Deck>>
    {
        
        
        private int _tile;
        private readonly string _email;
        public string Email
        {
            get { return _email; }
        }

        public int Tile
        {
            get { return _tile; }
        }
        public MoveTileCommand(string email, int tile)
        {
            _email = email;
            _tile = tile;
        }
    }
}
