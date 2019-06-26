using System.Collections.Generic;
using System.Linq;
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

namespace EP.DotsBoxes.Logic.Handlers
{
    public class UpdateGameBoardHandler : IRequestHandler<UpdateGameBoardCommand, Result<GameBoard>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;

        public UpdateGameBoardHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<GameBoard>> Handle(UpdateGameBoardCommand request, CancellationToken cancellationToken)
        {
            var cell = new Cell()
            {
                Row = request.Row,
                Column = request.Column,
                Top = request.Top,
                Bottom = request.Bottom,
                Right = request.Right,
                Left = request.Left
            };

            var context = _context.GameBoard.Include(b => b.Cells)
                .FirstOrDefault(b => b.Id == request.GameBoardId);

            var gameBoard = _mapper.Map<GameBoard>(context);
            var cellCollection = new GameLogic(gameBoard.Cells).AddCommonSide(cell, gameBoard.Rows, gameBoard.Columns);
            context.Cells = _mapper.Map<List<Cell>, List<CellDb>>(cellCollection);

            //_context.Entry(context).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Result.Ok<GameBoard>(gameBoard);
        }
    }
}
