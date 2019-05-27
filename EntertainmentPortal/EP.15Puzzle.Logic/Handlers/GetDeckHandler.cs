using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP._15Puzzle.Data;
using EP._15Puzzle.Logic.Queries;
using EP._15Puzzle.Logic.Services;
using MediatR;

namespace EP._15Puzzle.Logic.Handlers
{
    public class GetDeckHandler : IRequestHandler<GetDeck, Deck>
    {
        public Task<Deck> Handle(GetDeck request, CancellationToken cancellationToken)
        {
            var deck = DeckService.GetDeck(request.Id);

            return Task.FromResult(deck);
        }
    }
}
