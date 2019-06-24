using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using EP.WordsMaker.Logic.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EP.WordsMaker.Logic.Handlers
{
	public class GetPlayerHandler : IRequestHandler<GetPlayerCommand, Result<Player>>
	{
		private readonly IMapper _mapper;
		private readonly GameDbContext _context;

		public GetPlayerHandler(IMapper mapper, GameDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<Result<Player>> Handle(GetPlayerCommand request, CancellationToken cancellationToken)
		{
			var result = await _context.Players.FindAsync(request.Id);
			if (result == null)
			{
				return (Result.Fail<Player>("Player not found(handler)"));
			}

			return (Result.Ok(_mapper.Map<Player>(result)));
		}
	}
}
