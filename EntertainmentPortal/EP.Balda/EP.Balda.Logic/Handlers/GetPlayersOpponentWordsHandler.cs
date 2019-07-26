using AutoMapper;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    public class GetPlayersOpponentWordsHandler : IRequestHandler<GetPlayersOpponentWords, List<string>>
    {
        private readonly BaldaGameDbContext _context;

        public GetPlayersOpponentWordsHandler(BaldaGameDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetPlayersOpponentWords request, CancellationToken cancellationToken)
        {

            var words = await _context.PlayerWords
                .Where(m => m.GameId == request.GameId && m.PlayerId == request.PlayerId && m.IsChosenByOpponnent == true)
                .OrderBy(w => w.Id)
                .Select(w => w.Word.Word)
                .ToListAsync();
            
            return words == null
                ? new List<string>()
                : words;
        }
    }
}
