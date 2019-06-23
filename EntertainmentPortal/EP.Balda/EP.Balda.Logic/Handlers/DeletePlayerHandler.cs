using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EP.Balda.Logic.Handlers
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, Result<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;

        public DeletePlayerHandler(IMapper mapper, BaldaGameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Player>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerDb = await (_context.Players
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync());

            if(playerDb == null)
                return Result.Fail<Player>($"There is no player with id {request.Id} in DB");

            _context.Players.Remove(playerDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Player>(playerDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Player>(ex.Message);
            }
        }
    }
}