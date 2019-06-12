using AutoMapper;
using EP.Sudoku.Data;
using EP.Sudoku.Data.Context;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Sudoku.Logic.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayers, IEnumerable<Player>>
    {
        private readonly SudokuDbContext _context;
        private readonly IMapper _mapper;

        public GetAllPlayersHandler(SudokuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Player>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
        {
            var players = await _context.Players
                .Include(p => p.IconDb)
                .Include(p => p.GameSessionsDb)
                .Select(b => _mapper.Map<Player>(b)).ToListAsync()
                .ConfigureAwait(false); 
            
            return await Task.FromResult((IEnumerable<Player>)players);
        }
    }
}
