using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using EP._15Puzzle.Logic.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class ResetDeckHandler : IRequestHandler<ResetDeckCommand, Result<Deck>>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public ResetDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<Deck>> Handle(ResetDeckCommand request, CancellationToken cancellationToken)
        {
            var userExists = new ResetDeckValidator(_context).Validate(request);

            if (!userExists.IsValid)
            {
                return Result.Fail<Deck>(userExists.Errors.First().ErrorMessage);
            }

            try
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
                return Result.Ok<Deck>(_mapper.Map<Deck>(deck));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Deck>(ex.Message);
            }
            

        }

        private DeckDb Unsort(DeckDb deck) 
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
            deck.Victory = false;
            deck.Score = 0;
            
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
