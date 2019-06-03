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

            if (_context.Decks.Any(d => d.UserId == request.Id))
            {
                return await Task.FromResult(_mapper.Map<Deck>(_context.Decks.First(d => d.UserId == request.Id)));
            }
            return null;
            
        }
    }
}
