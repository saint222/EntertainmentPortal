using System;
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

namespace EP.Hangman.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, Result<ControllerData>>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        public CreateNewGameHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ControllerData>> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
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
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok<ControllerData>(_mapper.Map<UserGameData, ControllerData>(_mapper.Map<GameDb, UserGameData>(result)));
            }
            catch (DbUpdateException exception)
            {
                return Result.Fail<ControllerData>(exception.Message);
            }
        }
    }
}
