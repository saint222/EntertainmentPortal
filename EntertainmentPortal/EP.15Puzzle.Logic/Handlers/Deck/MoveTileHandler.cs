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
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class MoveTileHandler : IRequestHandler<MoveTileCommand, Result<Deck>>
    {
        private readonly DeckDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<MoveTileCommand> _validator;

        public MoveTileHandler(DeckDbContext context, IMapper mapper, IValidator<MoveTileCommand> validator)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
        }
        public async Task<Result<Deck>> Handle([NotNull]MoveTileCommand request, CancellationToken cancellationToken)
        {

            //validate tile
            var result = await _validator.ValidateAsync(request, ruleSet: "MoveTileSet", cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            if (!result.IsValid)
            {
                return Result.Fail<Deck>(result.Errors.First().ErrorMessage);
            }



            var user = await _context.UserDbs.AsNoTracking()
                .Include(u=>u.Deck)
                .Include(u => u.Deck.Tiles)
                .Include(u => u.Records)
                .FirstOrDefaultAsync(
                u => u.Email == request.Email, cancellationToken);

            if (user==null)
            {
                return Result.Fail<Deck>("There is no user with such id.");
            }

            try
            {
                var deckDb = user.Deck;

                var logicDeck = _mapper.Map<LogicDeck>(deckDb);
                logicDeck.SetNearbyTiles();
                if (logicDeck.TileCanMove(request.Tile))
                {
                    logicDeck.Move(request.Tile);
                    logicDeck.Tiles=logicDeck.Tiles.OrderBy(t => t.Pos).ToList();
                    logicDeck.Score += 1;
                    if (logicDeck.CheckWin())
                    {
                        logicDeck.Victory = true;

                        var recordDb = new RecordDb() {Score = logicDeck.Score, User = user};
                        logicDeck.User.Records.Add(recordDb);
                    }
                    _context.Update(_mapper.Map<DeckDb>(logicDeck));
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    return Result.Ok<Deck>(_mapper.Map<Deck>(logicDeck));
                }
                
                var strTiles = string.Join(',', logicDeck.Tiles.First(t => t.Num == 0).NearbyTiles.Select(t => t.Num));
                return Result.Fail<Deck>("Selected tile can not be moved. Possible: "+ strTiles);
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Deck>(ex.Message);
            }
        }


        
    }
}
