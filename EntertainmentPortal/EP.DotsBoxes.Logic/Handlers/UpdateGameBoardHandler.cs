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

namespace EP.DotsBoxes.Logic.Handlers
{
    public class UpdateGameBoardHandler : IRequestHandler<UpdateGameBoardCommand, Result<Cell>>
    {
        private readonly IMapper _mapper;
        private readonly GameBoardDbContext _context;
        
        public UpdateGameBoardHandler(IMapper mapper, GameBoardDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Cell>> Handle(UpdateGameBoardCommand request, CancellationToken cancellationToken)
        {
            var model = new Cell()
            {
                Row = request.Row,
                Column = request.Column,
                Top = request.Top,
                Bottom = request.Bottom,
                Left = request.Left,
                Right = request.Right
            };

            var context = _context.Cells
                .FirstOrDefault(c => c.Row == request.Row && c.Column == request.Column);
            context.Top = request.Top;
            context.Bottom = request.Bottom;
            context.Left = request.Left;
            context.Right = request.Right;
           _context.Entry(context).State = EntityState.Modified;
           
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Result.Ok<Cell>(model);
        }
    }
}
