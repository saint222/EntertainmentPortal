using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Logic.Handlers
{
	public class GetAllGamesHandler : IRequestHandler<GetAllGames, Maybe<IEnumerable<GameDb>>>
	{
		private readonly IMapper _mapper;
		private readonly GameDbContext _context;

		public GetAllGamesHandler(IMapper mapper, GameDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<Maybe<IEnumerable<GameDb>>> Handle(GetAllGames request, CancellationToken cancellationToken)
		{
			var result = await _context.Games
				.AsNoTracking()
				.ToArrayAsync(cancellationToken)
				.ConfigureAwait(false);
            
			return result.Any() ?
				Maybe<IEnumerable<GameDb>>.From(result) :
				Maybe<IEnumerable<GameDb>>.None;
		}
	}
}