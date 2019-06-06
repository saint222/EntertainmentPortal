using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class DeleteGameSessionCommand : IRequest<ControllerData>
    {
        public DeleteGameSessionCommand(ControllerData data)
        {
            _data = data;
        }

        public ControllerData _data { get; set; }
    }
}
