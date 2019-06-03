using System;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class CheckLetterCommand : IRequest<UserGameData>
    {
        public CheckLetterCommand(string id, string letter)
        {
            Id = Convert.ToInt64(id);
            Letter = letter.ToUpper();
        }

        public long Id { get; set; }
        public string Letter { get; set; }
    }
}
