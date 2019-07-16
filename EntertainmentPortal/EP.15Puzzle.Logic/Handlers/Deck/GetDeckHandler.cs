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
using EP._15Puzzle.Logic.Models;
using EP._15Puzzle.Logic.Queries;
using EP._15Puzzle.Logic.Validators;
using FluentValidation;
using JetBrains.Annotations;
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
            
            try
            {
                var deckDb = _context.UserDbs
                    .Include(d => d.Deck.Tiles)
                    .First(u => u.Email==request.Email).Deck;
                return await Task.FromResult(Result.Ok<Deck>(_mapper.Map<Deck>(deckDb)));
            }
            catch (DbException ex)
            {
                return Result.Fail<Deck>(ex.Message);
            }
        }
    }
}
