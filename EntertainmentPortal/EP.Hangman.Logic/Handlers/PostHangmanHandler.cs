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
    public class PostHangmanHandler : IRequestHandler<PostHangman, HangmanTemporaryData>
    {
        private HangmanTemporaryData _item;
        public PostHangmanHandler(HangmanTemporaryData item)
        {
            _item = item;
        }

        public Task<HangmanTemporaryData> Handle(PostHangman request, CancellationToken cancellationToken)
        {
            var repository = new Repository();

            return Task.FromResult(repository.Create(_item));
        }
    }
}
