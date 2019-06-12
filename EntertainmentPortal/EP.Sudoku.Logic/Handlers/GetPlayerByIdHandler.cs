using AutoMapper;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerById, Player>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public GetPlayerByIdHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Player> Handle(GetPlayerById request, CancellationToken cancellationToken)
        {
            var chosenPlayer = _context.Players
                .Include(p => p.IconDb)
                .Include(p => p.GameSessionsDb)                
                .Where(x => x.Id == request.Id)
                .Select(b => _mapper.Map<Player>(b)).FirstOrDefault();

            return await Task.FromResult(chosenPlayer);
        }
    }
}
