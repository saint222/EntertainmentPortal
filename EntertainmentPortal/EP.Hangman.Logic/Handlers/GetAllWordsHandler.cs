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
    public class GetAllWordsHandler : IRequestHandler<GetAllWords, IEnumerable<Word>>
    {
        private HangmanWordsData _item;
        public GetAllWordsHandler(HangmanWordsData item)
        {
            _item = item;
        }
        public Task<IEnumerable<Word>> Handle(GetAllWords request, CancellationToken cancellationToken)
        {
            var words = _item.AllWords.Select(a => new Word() {Name = a.Name}).ToArray();
            return Task.FromResult((IEnumerable<Word>) words);
        }
    }
}
