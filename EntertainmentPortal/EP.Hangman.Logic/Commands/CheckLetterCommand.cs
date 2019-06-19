using System;
using CSharpFunctionalExtensions;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class CheckLetterCommand : IRequest<Result<ControllerData>>
    {
        public ControllerData _data;

        public CheckLetterCommand(ControllerData data)
        {
            _data = data;
        }
    }
}
