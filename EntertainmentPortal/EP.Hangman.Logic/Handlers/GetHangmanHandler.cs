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
    public class GetHangmanHandler : IRequestHandler<GetHangman, PlayHangman>
    {
        private HangmanWordsData _item;
        public GetHangmanHandler(HangmanWordsData item)
        {
            _item = item;
        }

        public Task<PlayHangman> Handle(GetHangman request, CancellationToken cancellationToken)
        {
            var item = new PlayHangman();
            item.PickedWord = _item.GetWord.Name;
            item.CorrectLetters = new HangmanTemporaryData().TempData;
            item.Alphabet = new HangmanAlphabetData().Alphabet;
            return Task.FromResult(item);
        }
    }
}
