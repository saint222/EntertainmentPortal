using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Logic.Queries;
using EP._15Puzzle.Logic.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class GetDeckHandler : IRequestHandler<GetDeckQuery, Result<Deck>>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public GetDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<Deck>> Handle(GetDeckQuery request, CancellationToken cancellationToken)
        {
            var userExists = new GetDeckValidator(_context).Validate(request);
            if (!userExists.IsValid)
            {
                return Result.Fail<Deck>(userExists.Errors.First().ErrorMessage);
            }

            try
            {
                var deck = _context.UserDbs
                    .Include(d => d.Deck.Tiles)
                    .Include(d => d.Deck.EmptyTile)
                    .First(u => u.Id == request.Id).Deck;
                return await Task.FromResult(Result.Ok<Deck>(_mapper.Map<Deck>(deck)));
            }
            catch (DbException ex)
            {
                return Result.Fail<Deck>(ex.Message);
            }
        }
    }
}
