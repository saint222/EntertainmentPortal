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
	public class AddNewWordHandler : IRequestHandler<AddNewWordCommand, Result<Word>>
	{
		private readonly GameDbContext _context;
		private readonly IMapper _mapper;
		private readonly IValidator<AddNewWordCommand> _validator;
		private WordDictionary wordDictionary = new WordDictionary();
		private WordComparer wordComparer = new WordComparer();

		public AddNewWordHandler(GameDbContext context, IMapper mapper, IValidator<AddNewWordCommand> validator)
		{
			_context = context;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<Result<Word>> Handle(AddNewWordCommand request, CancellationToken cancellationToken)
		{
			var result = _validator.Validate(request);

			if (!result.IsValid)
			{
				return Result.Fail<Word>(result.Errors.First().ErrorMessage);
			}

			var temp = _context.Words.Where(x => x.GameId == request.GameId)
				.Where(x => x.Value == request.Value).ToListAsync();
			if (temp.Result.Count != 0)
			{
				return (Result.Fail<Word>("This word already exists(Word Handler)"));
			}

			string _word = request.Value;

			var Id = request.GameId;
			var _game = await _context.Games.FindAsync(Id);
			var _keyWord = _game.KeyWord;

			if (wordDictionary.Contains(_keyWord, _word))
			{
				var model = new WordDb
				{
					Id = Guid.NewGuid().ToString(),
					Value = request.Value,
					GameId = request.GameId
				};
				_context.Words.Add(model);
				try
				{
					await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
					return Result.Ok<Word>(_mapper.Map<Word>(model));
				}
				catch (DbUpdateException ex)
				{
					return Result.Fail<Word>(ex.Message);
				}
			}
			else
			{
				return (Result.Fail<Word>("This word incorrect (Word Handler)"));
			}
			
		}
	}
}