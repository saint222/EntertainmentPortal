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
    public class MoveTileHandler : IRequestHandler<MoveTile, Deck>
    {
        public Task<Deck> Handle(MoveTile request, CancellationToken cancellationToken)
        {
            var deck = DeckService.Move(request.Id, request.Tile);
            
            return Task.FromResult(deck);
        }
    }
}
