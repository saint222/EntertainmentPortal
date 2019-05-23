using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using EP.Hangman.Logic.Queries;
using EP.Hagman.Data;
using EP.Hangman.Logic.Models;
using System.Threading;
using System.Threading.Tasks;


namespace EP.Hangman.Logic.Handlers
{
    public class CheckLetterHandler : IRequestHandler<CheckLetter, PlayHangman>
    {
        private HangmanAlphabetData _alphabet;
        private HangmanTemporaryData _tempData;
        private HangmanWordsData _word;
        public CheckLetterHandler(HangmanAlphabetData alphabet, HangmanTemporaryData tempData, HangmanWordsData word)
        {
            _alphabet = alphabet;
            _tempData = tempData;
            _word = word;
        }

        public Task<PlayHangman> Handle(CheckLetter request, CancellationToken cancellationToken)
        {
            var item = new PlayHangman
            {
                PickedWord = _word.GetWord.Name,
                CorrectLetters = _tempData.TempData,
                Alphabet = _alphabet.Alphabet
            };
            var result = item.PlayGame(request.LetterToCheck);

            return Task.FromResult(item);

        }
    }
}
