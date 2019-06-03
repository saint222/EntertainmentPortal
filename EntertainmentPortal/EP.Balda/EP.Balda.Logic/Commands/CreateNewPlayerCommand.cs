using EP.Balda.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Balda.Logic.Commands
{
    public class CreateNewPlayerCommand : IRequest<Player>
    {
    }
}
