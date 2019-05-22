using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP.Hagman.Data.Models;
using MediatR;
using EP.Hangman.Logic.Models;
using EP.Hangman.Logic.Queries;
using EP.Hagman.Data;

namespace EP.Hangman.Logic.Handlers
{
    public class GetHangmanHendler : IRequestHandler<GetHangman, PlayHangman>
    {
        public Task<PlayHangman> Handle(GetHangman request, CancellationToken cancellationToken)
        {
            var item = new PlayHangman();
            item.PickedWord = HangmanWordsData.GetWord.Name;
            item.CorrectLetters = new HangmanTemporaryData().TempData;
            item.Alphabet = new HangmanAlphabetData().EnglishAlphabet();
            return Task.FromResult(item);
        }
    }
}
