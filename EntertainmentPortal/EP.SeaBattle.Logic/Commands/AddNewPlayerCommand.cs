using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Commands
{
    public class AddNewPlayerCommand : IRequest<Result<Player>>
    {
        public string NickName { get; set; }
        public bool CanShoot { get; set; }
    }
}
