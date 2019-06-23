using System;
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
    public class
        CreateNewPlayerHandler : IRequestHandler<CreateNewPlayerCommand, Result<Player>>
    {
        private readonly BaldaGameDbContext _context;
        private readonly IMapper _mapper;
        
        public CreateNewPlayerHandler(BaldaGameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Player>> Handle(CreateNewPlayerCommand request, CancellationToken cancellationToken)
        {
            var playerDb = new PlayerDb
            {
                NickName = request.NickName,
                Login = request.Login,
                Password = request.Password,
                Created = DateTime.UtcNow
            };

            _context.Players.Add(playerDb);

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