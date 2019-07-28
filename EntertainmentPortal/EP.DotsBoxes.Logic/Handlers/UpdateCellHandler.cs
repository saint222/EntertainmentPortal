using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using EP.DotsBoxes.Data.Models;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class UpdateGameBoardHandler : IRequestHandler<UpdateCellCommand, Result<IEnumerable<Cell>>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        private readonly ILogger<UpdateCellCommand> _logger;

        public UpdateGameBoardHandler(IMapper mapper, GameBoardDbContext context, ILogger<UpdateCellCommand> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<Cell>>> Handle(UpdateCellCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating the cells.");

            var model = new Cell()
            {
                Row = request.Row,
                Column = request.Column,
                Top = request.Top,
                Bottom = request.Bottom,
                Right = request.Right,
                Left = request.Left,
                Name = request.Name
            };

            var context = _context.GameBoard
                .Include(b => b.Cells)
                .Include(b => b.Players)
                .FirstOrDefault(b => b.Id == request.GameBoardId);

            var gameBoard = _mapper.Map<GameBoard>(context);
            var game = new GameLogic(gameBoard.Cells, gameBoard.Players);
            var sides = game.AddSides(model, gameBoard.Rows, gameBoard.Columns);
            var player = game.CheckSquare(sides);

            if (player != null) 
            {
                var players = context.Players.Where(b => b.Id == player.Id).FirstOrDefault();
                players.Score = player.Score;
                _context.Entry(players).State = EntityState.Modified;
            }
          
            foreach (var item in sides)
            {
                if (item != null)
                {
                    var cell = context.Cells.Where(b => b.Id == item.Id).FirstOrDefault();
                    cell.Top = item.Top;
                    cell.Bottom = item.Bottom;
                    cell.Right = item.Right;
                    cell.Left = item.Left;
                    _context.Entry(cell).State = EntityState.Modified;
                }
            }

            try
            {
                _logger.LogInformation("Updating database the cells.");
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Ok(_mapper.Map<IEnumerable<Cell>>(context.Cells));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "Unsuccessful database update the cells!");
                return Result.Ok(_mapper.Map<IEnumerable<Cell>>(context.Cells));
            }
        }
    }
}
