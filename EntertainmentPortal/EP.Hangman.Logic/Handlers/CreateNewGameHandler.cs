using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EP.Hangman.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<ControllerData>>
    {
        private readonly GameDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDatabaseHandler> _logger;

        public CreateNewGameHandler(GameDbContext context, IMapper mapper, ILogger<CreateDatabaseHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ControllerData>> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new game session");
            var item = new UserGameData()
            {
                PickedWord = new Word().GetNewWord().ToUpper(),
                Alphabet = new Alphabets().EnglishAlphabet(),
                CorrectLetters = new List<string>()
            };
            for (var i = 0; i < item.PickedWord.Length; i++)
            {
                item.CorrectLetters.Add("_");
            }
            item.UserErrors = 0;

            var result = _mapper.Map<UserGameData, GameDb>(item);

            _context.Games.Add(result);
            
            try
            {
                _logger.LogInformation("Updating database with new game session");
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Database was updated");

                return Result.Ok<ControllerData>(_mapper.Map<UserGameData, ControllerData>(_mapper.Map<GameDb, UserGameData>(result)));
            }
            catch (DbUpdateException exception)
            {
                _logger.LogError("Unsuccessful database update");
                return Result.Fail<ControllerData>(exception.Message);
            }
        }
    }
}
