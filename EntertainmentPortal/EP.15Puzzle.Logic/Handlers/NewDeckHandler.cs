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
using EP._15Puzzle.Logic.Queries;
using MediatR;

namespace EP._15Puzzle.Logic.Handlers
{
    public class NewDeckHandler : IRequestHandler<NewDeck, Deck>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public NewDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Deck> Handle(NewDeck request, CancellationToken cancellationToken)
        {
            var deck = new DeckDb()
            {
                Score = 0,
                Tiles = new List<int>() {16, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15},
                UserId = request.Id,
                Victory = false
            };

            do
            {
                deck = Unsort(deck);
            } while (!CheckWin(deck));

            
            if (_context.Decks.Any(d => d.UserId == request.Id))
            {
                var deckRef = _context.Decks.First(d => d.UserId == request.Id);
                deckRef.Tiles = deck.Tiles;
                deckRef.Victory = false;
                deckRef.Score = 0;
            }
            else
            {
                _context.Decks.Add(deck);
                
            }
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<Deck>(deck);

        }

        private DeckDb Unsort(DeckDb deck)
        {
            List<int> tiles = deck.Tiles;
            Random random = new Random();
            for (int i = 15; i >= 2; i--)
            {
                int j = random.Next(i) + 1;

                var temp = tiles[i];
                tiles[i] = tiles[j];
                tiles[j] = temp;
            }

            deck.Tiles = tiles;
            return deck;
        }

        private bool CheckWin(DeckDb deck)
        {
            int[] tilesOnDeck = new int[16];
            for (int i = 0; i <= 15; i++)
            {
                tilesOnDeck[i] = deck.Tiles.FindIndex(p => p == i);
            }

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
