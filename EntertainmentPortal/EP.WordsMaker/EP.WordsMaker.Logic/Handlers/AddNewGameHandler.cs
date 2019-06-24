using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Context;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EP.WordsMaker.Logic.Handlers
{
	public class AddNewGameHandler : IRequestHandler<AddNewGameCommand, Result<Game>>
	{
		private readonly GameDbContext _context;
		private readonly IMapper _mapper;
		private readonly IValidator<AddNewGameCommand> _validator;

		public AddNewGameHandler(GameDbContext context, IMapper mapper, IValidator<AddNewGameCommand> validator)
		{
			_context = context;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Result<Game>> Handle(AddNewGameCommand request, CancellationToken cancellationToken)
		{
			var result = _validator.Validate(request);

			if (!result.IsValid)
			{
				return Result.Fail<Game>(result.Errors.First().ErrorMessage);
			}

			var _player = await _context.Players.FindAsync(request.Id);

			if(_player == null)
			{
				return (Result.Fail<Game>("Player not found(Game handler)"));
			}
			var model = new GameDb
			{
				StartTime = DateTime.UtcNow,
				//PlayerId = request.Id,
				Player = _player //await _context.Players.FindAsync(request.Id)
			};

			_context.Games.Add(model);

			try
			{
				await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
				return Result.Ok<Game>(_mapper.Map<Game>(model));
			}
			catch (DbUpdateException ex)
			{
				return Result.Fail<Game>(ex.Message);
			}
		}
	}
}