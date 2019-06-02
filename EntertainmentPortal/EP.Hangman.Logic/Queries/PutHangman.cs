using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Data;
using MediatR;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Queries
{
    public class PutHangman : IRequest<UserGameData>
    {
        public PutHangman(string letter)
        {
            LetterToCheck = letter;
        }

        public string LetterToCheck { get; set; }
    }
}
