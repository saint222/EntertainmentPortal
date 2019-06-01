using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Logic.Commands;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class CreateDataBaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly PlayerDbContext _context;

        public CreateDataBaseHandler(PlayerDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }
    }
}
