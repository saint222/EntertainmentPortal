using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP._15Puzzle.Data.Context;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP._15Puzzle.Logic.Handlers
{
    public class GetLeaderboardHandler : IRequestHandler<GetLeaderboardCommand, Result<Record>>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;
        

        public GetLeaderboardHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<Record>> Handle(GetLeaderboardCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var decks = _context.RecordDbs
                    .Include(d => d.User).OrderByDescending(r=>r.Score).Take(10);
                return await Task.FromResult(Result.Ok<Record>(_mapper.Map<Record>(decks)));
            }
            catch (DbException ex)
            {
                return Result.Fail<Record>(ex.Message);
            }
        }
    }
}
