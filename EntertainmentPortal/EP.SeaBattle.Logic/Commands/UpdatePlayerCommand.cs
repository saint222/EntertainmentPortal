using CSharpFunctionalExtensions;
using EP.SeaBattle.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.SeaBattle.Logic.Commands
{
    public class UpdatePlayerCommand : IRequest<Result<Player>>
    {
        public string Id { get; set; }
        public string NickName { get; set; }
    }
}
