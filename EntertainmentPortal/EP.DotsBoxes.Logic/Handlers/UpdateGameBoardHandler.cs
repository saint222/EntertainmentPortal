using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class UpdateGameBoardHandler : IRequestHandler<UpdateGameBoardCommand, Result<GameBoard>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        private readonly ILogger<UpdateGameBoardCommand> _logger;

        public UpdateGameBoardHandler(IMapper mapper, GameBoardDbContext context, ILogger<UpdateGameBoardCommand> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<GameBoard>> Handle(UpdateGameBoardCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating the game board.");

            var cell = new Cell()
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
            var sides = game.AddSides(cell, gameBoard.Rows, gameBoard.Columns);
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
                    var cells = context.Cells.Where(b => b.Id == item.Id).FirstOrDefault();
                    cells.Top = item.Top;
                    cells.Bottom = item.Bottom;
                    cells.Right = item.Right;
                    cells.Left = item.Left;
                    _context.Entry(cells).State = EntityState.Modified;
                }
            }
                                    
            try
            {
                _logger.LogInformation("Updating database the game board.");
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Ok<GameBoard>(_mapper.Map<GameBoard>(context));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, "Unsuccessful database update the game board!");
                return Result.Ok<GameBoard>(gameBoard);
            }
        }
    }
}
