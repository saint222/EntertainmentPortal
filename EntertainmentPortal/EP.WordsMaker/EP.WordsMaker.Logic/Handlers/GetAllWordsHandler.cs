using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using EP.WordsMaker.Data;
using EP.WordsMaker.Logic.Queries;
using EP.WordsMaker.Logic.Models;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EP.WordsMaker.Logic.Handlers
{
    public class GetAllWordsHandler : IRequestHandler<GetAllWordsCommand, Result<IEnumerable<Word>>>
    {
        private readonly IMapper _mapper;
        private readonly GameDbContext _context;

        public GetAllWordsHandler(IMapper mapper, GameDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<IEnumerable<Word>>> Handle(GetAllWordsCommand request, CancellationToken cancellationToken)
        {
	        var result = await _context.Words
						.Where(x=> x.GameId == request.GameId)
			            .AsNoTracking()
			            .ToArrayAsync(cancellationToken)
			            .ConfigureAwait(false);

			if (result.Length == 0)
			{
				return (Result.Fail<IEnumerable<Word>>("Words array is empty(handler)"));
			}

			return (Result.Ok(_mapper.Map<IEnumerable<Word>>(result)));
		}
    }
}
