using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Data.Models;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, Player>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePlayerHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Player> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerDb = _mapper.Map<PlayerDb>(request.player);
            playerDb.IconDb = _context.Find<AvatarIconDb>(request.player.Icon.Id);
            _context.Entry(playerDb).State = EntityState.Modified;            
            await _context.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(request.player);
        }
    }
}
