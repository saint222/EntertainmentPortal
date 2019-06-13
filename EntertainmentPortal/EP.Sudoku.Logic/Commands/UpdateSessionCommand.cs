using EP.Sudoku.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Commands
{
    public class UpdateSessionCommand : IRequest<Session>
    {
        public Session session { get; set; }
        public UpdateSessionCommand(Session s)
        {
            session = s;
        }
    }    
}
