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
    public class NewDeckHandler : IRequestHandler<NewDeckCommand, Deck>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public NewDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Deck> Handle(NewDeckCommand request, CancellationToken cancellationToken)
        {
            var deck = new DeckDb(4);

            do
            {
                deck = Unsort(deck);
            } while (!CheckWinIsPossible(deck));
            
            _context.Add(deck);
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
            for (int i = 1; i <= 15; i++)
            {
                tilesOnDeck[i] = deck.Tiles.First(p => p.Pos==i).Num;
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
