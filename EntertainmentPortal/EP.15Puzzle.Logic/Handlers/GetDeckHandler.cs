using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP._15Puzzle.Data;
using EP._15Puzzle.Logic.Queries;
using MediatR;

namespace EP._15Puzzle.Logic.Handlers
{
    public class GetDeckHandler : IRequestHandler<GetDeck, Deck>
    {
        private readonly IMapper _mapper;

        public GetDeckHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public Task<Deck> Handle(GetDeck request, CancellationToken cancellationToken)
        {
            var deck = DeckRepository.Get(request.Id);
            return Task.FromResult(_mapper.Map<Deck>(deck));
            
        }
    }
}
