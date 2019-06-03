using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.Hangman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Data.Context;
using EP.Hangman.Logic.Commands;

namespace EP.Hangman.Logic.Handlers
{
    public class CreateNewGameHandler : IRequestHandler<CreateNewGameCommand, UserGameData>
    {
        private readonly GameDbContext _context;

        private readonly IMapper _mapper;

        public CreateNewGameHandler(GameDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserGameData> Handle(CreateNewGameCommand request, CancellationToken cancellationToken)
        {
            var item = new GameDb
            {
                PickedWord = new Word().GetNewWord().ToUpper(),
                Alphabet = new Alphabets().EnglishAlphabet(),
                CorrectLetters = new List<string>()
            };
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
