using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
		private WordDictionary wordDictionary = new WordDictionary();

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

			var _player = await _context.Players.FindAsync(request.PlayerId);

			if(_player == null)
			{
				return (Result.Fail<Game>("Player not found(Game handler)"));
			}

			var PlayerId = request.PlayerId;
			var res = _context.Games.Where(x => x.PlayerId == PlayerId).ToListAsync();
			//var _game = await _context.Games.FindAsync();
			if (res.Result.Count != 0)
			{
				var _game = res.Result.First();
				try
				{
					return Result.Ok<Game>(_mapper.Map<Game>(_game));
				}
				catch (DbUpdateException ex)
				{
					return Result.Fail<Game>(ex.Message);
				}
			}
			var model = new GameDb
			{
				StartTime = DateTime.UtcNow,
				Id = Guid.NewGuid().ToString(),
				KeyWord = wordDictionary.GetRandomKeyWord(),
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