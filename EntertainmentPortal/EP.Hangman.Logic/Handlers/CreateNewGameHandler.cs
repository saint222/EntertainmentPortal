using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hangman.Data;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;

namespace EP.Hangman.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, UserGameData>
    {
        private GameDbContext _context;
        private IMapper _mapper;
        public CreateNewGameHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserGameData> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            var item = new GameDb();
            item.PickedWord = new Word().GetNewWord().ToUpper();
            item.Alphabet = new Alphabets().EnglishAlphabet();
            item.CorrectLetters = new List<string>();
            for (int i = 0; i < item.PickedWord.Length; i++)
            {
                item.CorrectLetters.Add("_");
            }
            item.UserErrors = 0;

            _context.Games.Add(item);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<GameDb, UserGameData>(item);
        }
    }
}
