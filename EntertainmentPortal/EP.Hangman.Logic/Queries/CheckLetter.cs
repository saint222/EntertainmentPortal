using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Queries
{
    public class CheckLetter : IRequest<PlayHangman>
    {
        public CheckLetter(string letter)
        {
            LetterToCheck = letter;
        }

        public string LetterToCheck { get; set; }
    }
}
