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

            try
            {
                var deck = _context.DeckDbs
                    .Include(d => d.Tiles)
                    .Include(d => d.EmptyTile)
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

                return Result.Ok<Deck>(_mapper.Map<Deck>(deck));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Deck>(ex.Message);
            }
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
