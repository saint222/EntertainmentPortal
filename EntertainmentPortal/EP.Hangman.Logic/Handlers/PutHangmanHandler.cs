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
    public class PutHangmanHandler : IRequestHandler<PutHangman, HangmanTemporaryData>
    {
        private HangmanTemporaryData _item;
        public PutHangmanHandler(HangmanTemporaryData item)
        {
            _item = item;
        }
        public Task<HangmanTemporaryData> Handle(PutHangman request, CancellationToken cancellationToken)
        {
            var repository = new Repository();
            return Task.FromResult(repository.Update(_item, request.LetterToCheck));

        }
    }
}
