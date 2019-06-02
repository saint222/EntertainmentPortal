using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Data;
using EP.Hangman.Data.Context;

namespace EP.Hangman.Logic.Handlers
{
    public class GetUserSessionHandler : IRequestHandler<GetUserSession, UserGameData>
    {
        private GameDbContext _context;
        private IMapper _mapper;
        public GetUserSessionHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserGameData> Handle(GetUserSession request, CancellationToken cancellationToken)
        {
            return _mapper.Map<GameDb, UserGameData>(await _context.Games.FindAsync(request.Id, cancellationToken).ConfigureAwait(false));
        }
    }
}
