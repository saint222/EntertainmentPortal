using System.Linq;
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
    public class AddNewStepHandler : IRequestHandler<AddNewStepCommand, Result<Cell>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public AddNewStepHandler(TicTacDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Cell>> Handle(AddNewStepCommand request,
                                               CancellationToken cancellationToken)
        {
            var targetSymbol = 0;

            var firstPlayer = await _context.FirstPlayers
                .Where(p => p.HaunterId == request.PlayerId.ToString())
                .Where(x => x.Player.GameId == request.GameId)
                .FirstOrDefaultAsync(cancellationToken);
            SecondPlayerDb secondPlayer = null;
            if (firstPlayer != null) targetSymbol = firstPlayer.TicTac;

            secondPlayer = await _context.SecondPlayers
                .Where(p => p.HaunterId == request.PlayerId.ToString())
                .Where(x => x.Player.GameId == request.GameId)
                .FirstOrDefaultAsync(cancellationToken);
            if (secondPlayer != null) targetSymbol = secondPlayer.TicTac;


            if (firstPlayer == null && secondPlayer == null)
                return Result.Fail<Cell>(
                    $"This game has no player ID {request.PlayerId}");

            var mapDb = await _context.Maps
                .Where(c => c.GameId == request.GameId)
                .FirstOrDefaultAsync(cancellationToken);

            var cellDb = await _context.Cells
                .Where(c => c.MapId == mapDb.Id).Where(x => x.X == request.X)
                .Where(y => y.Y == request.Y)
                .FirstOrDefaultAsync(cancellationToken);

            if (cellDb == null)
                return Result.Fail<Cell>($"This Cell with X={request.X} Y={request.Y} " +
                                         $"in the game with ID [{request.GameId}], " +
                                         $"on this map with ID [{mapDb.Id}] does not exist.");

            if (cellDb.TicTac != 0)
                return Result.Fail<Cell>("The cell is already taken.");

            cellDb.TicTac = targetSymbol;
            _context.Cells.Update(cellDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<Cell>(cellDb));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<Cell>(ex.Message);
            }
        }
    }
}