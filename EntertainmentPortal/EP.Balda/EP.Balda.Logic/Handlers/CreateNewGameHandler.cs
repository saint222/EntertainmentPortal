using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.Balda.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<Game>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;
        
        public CreateNewGameHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Game>> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            //TODO: add players
            
            Map map = new Map(5);
            MapDb mapDb = _mapper.Map<MapDb>(map);
            await _context.Maps.AddAsync(mapDb);

            var gameDb = new GameDb()
            {
                Map = mapDb,
                MapId = mapDb.Id
            };

            await _context.Games.AddAsync(gameDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok(_mapper.Map<Game>(gameDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Game>(ex.Message);
            }
        }
    }
}