using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class MoveTileCommand : IRequest <Result<Deck>>
    {
        
        
        private int _tile;
        private readonly string _sub;
        public string Sub
        {
            get { return _sub; }
        }

        public int Tile
        {
            get { return _tile; }
        }
        public MoveTileCommand(string sub, int tile)
        {
            _sub = sub;
            _tile = tile;
        }
    }
}
