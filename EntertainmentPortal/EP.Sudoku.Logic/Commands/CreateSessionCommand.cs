using System;
using System.Collections.Generic;
using System.Text;
using EP.Sudoku.Logic.Enums;
using EP.Sudoku.Logic.Models;
using MediatR;

namespace EP.Sudoku.Logic.Commands
{
    public class CreateSessionCommand : IRequest<Session>
    {
        public Session session { get; set; }
        public CreateSessionCommand(Session s)
        {
            session = s;
        }
    }
}
