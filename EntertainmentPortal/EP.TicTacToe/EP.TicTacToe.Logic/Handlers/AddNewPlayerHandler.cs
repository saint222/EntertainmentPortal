using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Data.Models;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.TicTacToe.Logic.Handlers
{
    public class
        CreateNewPlayerHandler : IRequestHandler<AddNewPlayerCommand, Result<Player>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public CreateNewPlayerHandler(TicTacDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Player>> Handle(AddNewPlayerCommand request,
                                                 CancellationToken cancellationToken)
        {
            var playerDb = new PlayerDb
            {
                UserName = request.NickName,
                Login = request.Login,
                Password = request.Password
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