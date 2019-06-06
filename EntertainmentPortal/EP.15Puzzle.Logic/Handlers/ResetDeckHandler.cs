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
    public class ResetDeckHandler : IRequestHandler<ResetDeck, Deck>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public ResetDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Deck> Handle(ResetDeck request, CancellationToken cancellationToken)
        {
            var deck = _context.DeckDbs
                .Include(d => d.Tiles)
                .Include(d => d.EmptyTile)
                .First(d => d.UserId == request.Id);

            do
            {
                deck = Unsort(deck);
            } while (!CheckWinIsPossible(deck));

            _context.Update(deck);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<Deck>(deck);

        }

        private DeckDb Unsort(DeckDb deck) //need tiles???
        {
            List<TileDb> tiles = deck.Tiles.ToList();
            Random random = new Random();
            for (int i = 14; i >= 0; i--)
            {
                int j = random.Next(i) + 1;

                var temp = tiles[i].Pos;
                tiles[i].Pos = tiles[j].Pos;
                tiles[j].Pos = temp;
            }

            deck.Tiles = tiles;
            return deck;
        }

        private bool CheckWinIsPossible(DeckDb deck)
        {
            int[] tilesOnDeck = new int[16];
            
            foreach (var tile in deck.Tiles)
            {
                tilesOnDeck[tile.Pos-1] = tile.Num;
            }

            tilesOnDeck[0] = deck.EmptyTile.Num;

            int rowOfEmpty = 0;
            for (int i = 0; i < 16; i++)
            {
                if (tilesOnDeck[i]==0)
                {
                    rowOfEmpty = i/4+1;
                    break;
                }
            }
            int chetnost = 0;
            for (int i = 0; i < 16; i++)
            {
                if (tilesOnDeck[i]!=0)
                {
                    int c = 0;
                    for (int j = i + 1; j < 16; j++)
                    {
                        if (tilesOnDeck[i] > tilesOnDeck[j])
                        {
                            c += 1;
                        }
                    }

                    chetnost += c;
                }
                
            }

            chetnost += rowOfEmpty;
            if (chetnost%2==0)
            {
                return true;
            }
            return false;
        }
    }
}
