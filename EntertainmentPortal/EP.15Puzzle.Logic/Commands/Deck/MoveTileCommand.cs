using CSharpFunctionalExtensions;
using EP._15Puzzle.Logic.Models;
using MediatR;

namespace EP._15Puzzle.Logic.Commands
{
    public class MoveTileCommand : IRequest <Result<Deck>>
    {
        
        
        private int _tile;
        private string _authType;
        private string _authId;
        public string AuthId
        {
            get { return _authId; }
        }
        public string AuthType
        {
            get { return _authType; }
        }

        public int Tile
        {
            get { return _tile; }
        }
        public MoveTileCommand(string authType, string authId, int tile)
        {
            _authType = authType;
            _authId = authId;
            _tile = tile;
        }
    }
}
