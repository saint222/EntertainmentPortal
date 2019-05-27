using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Queries
{
    public class SetWord : IRequest<Word>
    {
        public SetWord(string word)
        {
            SavedWord = word;
        }

        public string SavedWord { get; set; }
    }
}
