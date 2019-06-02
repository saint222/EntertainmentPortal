using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using MediatR;

namespace EP._15Puzzle.Logic.Handlers
{
    public class MoveTileHandler : IRequestHandler<MoveTile, Deck>
    {
        private readonly IMapper _mapper;

        public MoveTileHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<Deck> Handle(MoveTile request, CancellationToken cancellationToken)
        {
            var deck  = DeckRepository.Get(request.Id);
            var tiles = deck.Tiles;
            var tile = new Tile(tiles[request.Tile]);
            var tile0 = new Tile(tiles[0]);
            if (ComparePositions(tile, tile0))
            {
                var temp = deck.Tiles[0];
                deck.Tiles[0] = deck.Tiles[request.Tile];
                deck.Tiles[request.Tile] = temp;
                deck.Score += 1;
                if (CheckWin(deck))
                {
                    deck.Victory = true;
                }
                return Task.FromResult(_mapper.Map<Deck>(DeckRepository.Update(deck)));
            }
            return Task.FromResult(_mapper.Map<Deck>(deck));
        }

        /// <summary>
        /// check if it possible to move selected tile to move into empty place (to swap with tile0)
        /// </summary>
        /// <param name="tile">position of selected tile</param>
        /// <param name="tile0">position of empty place</param>
        /// <returns>true - swap is possible, tiles are touching in one row or column</returns>
        private bool ComparePositions(Tile tile, Tile tile0)
        {
            if (tile.PosX == tile0.PosX || tile.PosY == tile0.PosY)
            {
                if (Math.Abs(tile.PosX - tile0.PosX) == 1 || Math.Abs(tile.PosY - tile0.PosY) == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckWin(DeckDb deck)
        {
            for (int i = 1; i < deck.Tiles.Count; i++)
            {
                if (deck.Tiles[i]!=i)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
