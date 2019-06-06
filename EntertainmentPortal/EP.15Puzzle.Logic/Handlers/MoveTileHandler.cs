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
using Microsoft.EntityFrameworkCore;

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
            var deck = _context.DeckDbs
                .Include(d=>d.Tiles)
                .Include(d=>d.EmptyTile)
                .First(d => d.UserId == request.Id);
            var tile = deck.Tiles.First(t => t.Num == request.Tile);
            var tile0 = deck.EmptyTile;
            if (ComparePositions(tile, tile0))
            {
                var temp = tile0.Pos;
                deck.EmptyTile.Pos = tile.Pos;
                tile.Pos = temp;
                
                deck.Score += 1;
                if (CheckWin(deck))
                {
                    deck.Victory = true;
                }

                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return _mapper.Map<Deck>(deck);
        }


        private bool ComparePositions(TileDb tile, TileDb tile0)
        {
            var dif = Math.Abs(tile.Pos - tile0.Pos);
            if (dif == 1 || dif == 4)
            {
                return true;
            }
            return false;
        }

        private bool CheckWin(DeckDb deck)
        {
            foreach (var tile in deck.Tiles)
            {
                if (tile.Num!=tile.Pos)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
