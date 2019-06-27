using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class DeleteGameSessionCommand : IRequest<Result<ControllerData>>
    {
        public DeleteGameSessionCommand(string id)
        {
            _data.Id = Convert.ToInt64(id);
        }

        public ControllerData _data;
    }
}
