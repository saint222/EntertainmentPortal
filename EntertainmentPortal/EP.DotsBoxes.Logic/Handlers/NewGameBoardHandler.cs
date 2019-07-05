using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus.Extensions;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using EP.DotsBoxes.Logic.Queries;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class NewGameBoardHandler : IRequestHandler<NewGameBoardCommand, Result<GameBoard>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        
        public NewGameBoardHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<GameBoard>> Handle(NewGameBoardCommand request, CancellationToken cancellationToken)
        {
            var model = new GameBoard()
            {
                Rows = request.Rows,
                Columns = request.Columns,
                Cells = CreateGameBoard(request.Rows,request.Columns)
            };

            _context.GameBoard.Add(_mapper.Map<GameBoardDb>(model));
            
            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok<GameBoard>(_mapper.Map<GameBoard>(model));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<GameBoard>(ex.Message);
            }
        }

        private List<Cell> CreateGameBoard(int row, int column)
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= column; j++)
                {
                    cells.Add(new Cell() {Row = i, Column = j});
                }
            }

            return cells;
        }
    }
}
