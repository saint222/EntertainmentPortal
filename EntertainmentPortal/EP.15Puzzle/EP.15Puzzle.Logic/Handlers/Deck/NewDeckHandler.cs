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
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class NewDeckHandler : IRequestHandler<NewDeckCommand, Result<Deck>>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public NewDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<Deck>> Handle(NewDeckCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.UserDbs
                .Include(u=>u.Deck)
                .Include(d=>d.Deck.Tiles)
                .AsNoTracking()
                .FirstOrDefaultAsync(
                u => u.Email == request.Email, cancellationToken);
            if (user==null)
            {
                //create user and deck
                var logicDeck = new LogicDeck(4);

                do
                {
                    logicDeck.Unsort();
                } while (!logicDeck.CheckWinIsPossible());
                logicDeck.Tiles = logicDeck.Tiles.OrderBy(t => t.Pos).ToList();
                logicDeck.User.Email = request.Email;
                logicDeck.User.UserName = request.UserName;

                var deckDb = _mapper.Map<DeckDb>(logicDeck);
                _context.Add(deckDb);
                try
                {
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    return Result.Ok<Deck>(_mapper.Map<Deck>(deckDb));
                }
                catch (DbUpdateException ex)
                {
                    return Result.Fail<Deck>(ex.Message);
                }
            }

            //reset deck
            try
            {

                var deckDb = user.Deck;

                var logicDeck = _mapper.Map<LogicDeck>(deckDb);
                logicDeck.User.Email = request.Email;
                logicDeck.User.UserName = request.UserName;
                do
                {
                    logicDeck.Unsort();
                } while (!logicDeck.CheckWinIsPossible());
                logicDeck.Tiles = logicDeck.Tiles.OrderBy(t => t.Pos).ToList();
                logicDeck.Victory = false;
                logicDeck.Score = 0;
                _context.Update(_mapper.Map<DeckDb>(logicDeck));
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok<Deck>(_mapper.Map<Deck>(logicDeck));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Deck>(ex.Message);
            }

        }
    }
}
