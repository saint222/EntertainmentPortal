using System;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class CheckLetterCommand : IRequest<ControllerData>
    {
        public ControllerData _data { get; set; }

        public CheckLetterCommand(ControllerData data)
        {
            _data = data;
        }
    }
}
