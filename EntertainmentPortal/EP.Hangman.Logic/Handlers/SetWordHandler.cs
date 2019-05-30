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
    public class SetWordHandler : IRequestHandler<SetWord, Word>
    {
        private HangmanWordsData _item;
        public SetWordHandler(HangmanWordsData item)
        {
            _item = item;
        }

        public Task<Word> Handle(SetWord request, CancellationToken cancellationToken)
        {

            var word = new Word()
            {
                Name = request.SavedWord
            };
            //_item.AddWord(word.Name);
            return Task.FromResult(word);
        }
    }
}
