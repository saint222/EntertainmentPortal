using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using MediatR;

namespace EP._15Puzzle.Logic.Handlers
{
    public class MoveTileHandler : IRequestHandler<MoveTile, Deck>
    {
        private readonly DeckDbContext _context;
        private readonly IMapper _mapper;

        public MoveTileHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Deck> Handle(MoveTile request, CancellationToken cancellationToken)
        {
            var deck = _context.Decks.First(d => d.UserId == request.Id);
            var tiles = deck.Tiles;
            var tile = new Models.Tile(tiles[request.Tile]);
            var tile0 = new Models.Tile(tiles[0]);
            if (ComparePositions(tile, tile0))
            {
                var temp = tiles[0];
                tiles[0] = tiles[request.Tile];
                tiles[request.Tile] = temp;
                deck.Tiles = tiles;
                deck.Score += 1;
                if (CheckWin(deck))
                {
                    deck.Victory = true;
                }

                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return _mapper.Map<Deck>(deck);
        }

        /// <summary>
        /// check if it possible to move selected tile to move into empty place (to swap with tile0)
        /// </summary>
        /// <param name="tile">position of selected tile</param>
        /// <param name="tile0">position of empty place</param>
        /// <returns>true - swap is possible, tiles are touching in one row or column</returns>
        private bool ComparePositions(Models.Tile tile, Models.Tile tile0)
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
