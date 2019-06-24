using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using MediatR;

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
			var result = await _context.Players.FindAsync(request.Id);
			if (result == null)
			{
				return (Result.Fail<Game>("Game not found(handler)"));
			}

			return (Result.Ok(_mapper.Map<Game>(result)));
		}
	}
}