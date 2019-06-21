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
    public class NewDeckHandler : IRequestHandler<NewDeckCommand, Tuple<Result<Deck>, string>>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public NewDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Tuple<Result<Deck>, string>> Handle(NewDeckCommand request, CancellationToken cancellationToken)
        {

            var logicDeck = new LogicDeck(4);

            do
            {
                logicDeck.Unsort();
            } while (!logicDeck.CheckWinIsPossible());

            var deckDb = _mapper.Map<DeckDb>(logicDeck);
            _context.Add(deckDb);
            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateException ex)
            {
                
                return new Tuple<Result<Deck>, string>(Result.Fail<Deck>(ex.Message), deckDb.UserId.ToString());
            }
            return new Tuple<Result<Deck>, string>(Result.Ok<Deck>(_mapper.Map<Deck>(deckDb)), deckDb.UserId.ToString());

        }
    }
}
