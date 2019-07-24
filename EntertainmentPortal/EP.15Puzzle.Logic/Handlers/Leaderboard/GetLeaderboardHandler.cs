using System.Collections.Generic;
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
    public class GetLeaderboardHandler : IRequestHandler<GetLeaderboardCommand, Result<IEnumerable<Record>>>
    {
        private readonly IMapper _mapper;
        private readonly DeckDbContext _context;

        public GetLeaderboardHandler(DeckDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<IEnumerable<Record>>> Handle(GetLeaderboardCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var records = await _context.RecordDbs
                    .Include(d => d.User).OrderBy(r => r.Score).Take(10).AsNoTracking()
                    .ToArrayAsync(cancellationToken)
                    .ConfigureAwait(false);
                var rec = records.ToList();
                if (request.Email!=null && records.FirstOrDefault(r => r.User.Email == request.Email)==null)
                {
                    var my_records = await _context.RecordDbs
                        .Include(d => d.User).FirstOrDefaultAsync(r => r.User.Email == request.Email);
                    if (my_records!=null)
                    {
                        rec.Add(my_records);
                    }
                }
                return await Task.FromResult(Result.Ok<IEnumerable<Record>>(rec.Select(d => _mapper.Map<Record>(d))));
            }
            catch (DbException ex)
            {
                return Result.Fail<IEnumerable<Record>>(ex.Message);
            }
        }
    }
}
