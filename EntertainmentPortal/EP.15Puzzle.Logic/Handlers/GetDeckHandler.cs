using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class GetDeckHandler : IRequestHandler<GetDeck, Deck>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public GetDeckHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Deck> Handle(GetDeck request, CancellationToken cancellationToken)
        {

            if (_context.UserDbs.Any(d => d.Id == request.Id))
            {
                var deck = _context.UserDbs
                    .Include(d => d.Deck.Tiles)
                    .Include(d=>d.Deck.EmptyTile)
                    .First(u => u.Id == request.Id).Deck;

                return await Task.FromResult(_mapper.Map<Deck>(deck));
            }
            return null;
            
        }
    }
}
