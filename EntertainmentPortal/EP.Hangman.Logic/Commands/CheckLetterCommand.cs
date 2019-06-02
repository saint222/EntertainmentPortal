using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class CheckLetterCommand : IRequest<UserGameData>
    {
        public CheckLetterCommand(string ID, string letter)
        {
            Id = Convert.ToInt64(ID);
            Letter = letter;
        }

        public Int64 Id { get; set; }
        public string Letter { get; set; }
    }
}
