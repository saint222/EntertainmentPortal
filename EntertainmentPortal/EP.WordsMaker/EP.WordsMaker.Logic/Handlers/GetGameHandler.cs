using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Logic.Handlers
{
	public class GetGameHandler : IRequestHandler<GetGameCommand, Result<Game>>
	{
		private readonly IMapper _mapper;
		private readonly GameDbContext _context;

		public GetGameHandler(IMapper mapper, GameDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<Result<Game>> Handle(GetGameCommand request, CancellationToken cancellationToken)
		{
			var game = _context.Games.Where(x => x.PlayerId == request.PlayerId).FirstOrDefault();
			if (game == null)
			{
				return (Result.Fail<Game>("Game not found (Game handler)"));
			}
			return (Result.Ok(_mapper.Map<Game>(game)));
		}
	}
}