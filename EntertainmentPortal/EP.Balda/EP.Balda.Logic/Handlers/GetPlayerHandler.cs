using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Models;
using EP.Balda.Logic.Queries;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EP.Balda.Data.Models;

namespace EP.Balda.Logic.Handlers
{
    public class GetPlayerHandler : IRequestHandler<GetPlayer, Maybe<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Maybe<Player>> Handle(GetPlayer request, CancellationToken cancellationToken)
        {
<<<<<<< HEAD
            var playerDb = await (_context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync<PlayerDb>());
=======
            var playerDb = await _context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
>>>>>>> dev_s

            return playerDb == null ? 
                Maybe<Player>.None : 
                Maybe<Player>.From(_mapper.Map<Player>(playerDb));
        }
    }
}