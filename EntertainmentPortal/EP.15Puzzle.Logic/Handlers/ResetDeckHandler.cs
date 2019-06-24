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
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class ResetDeckHandler : IRequestHandler<ResetDeckCommand, Result<Deck>>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<ResetDeckCommand> _validator;
        private readonly DeckDbContext _context;

        public ResetDeckHandler(DeckDbContext context, IMapper mapper, IValidator<ResetDeckCommand> validator)
        {
            _mapper = mapper;
            _validator = validator;
            _context = context;
        }
        public async Task<Result<Deck>> Handle(ResetDeckCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator
                .ValidateAsync(request, ruleSet: "IdExistingSet", cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            if (!result.IsValid)
            {
                return Result.Fail<Deck>(result.Errors.First().ErrorMessage);
            }

            try
            {
                var deckDb = _context.DeckDbs.AsNoTracking()
                    .Include(d => d.Tiles)
                    .First(d => d.UserId == request.Id);
                var logicDeck = _mapper.Map<LogicDeck>(deckDb);
                do
                {
                    logicDeck.Unsort();
                } while (!logicDeck.CheckWinIsPossible());
                logicDeck.Tiles = logicDeck.Tiles.OrderBy(t => t.Pos).ToList();
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
