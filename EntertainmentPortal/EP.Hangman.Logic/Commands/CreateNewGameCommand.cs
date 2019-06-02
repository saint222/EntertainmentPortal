using System;
using System.Collections.Generic;
using System.Text;
using EP.Hangman.Logic.Models;
using MediatR;

namespace EP.Hangman.Logic.Commands
{
    public class CreateNewGameCommand : IRequest<UserGameData>
    {

    }
}
