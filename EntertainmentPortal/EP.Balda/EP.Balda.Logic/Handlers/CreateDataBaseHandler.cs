using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using Fody;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Logic.Handlers
{
    [ConfigureAwait(true)]
    public class CreateDataBaseHandler : AsyncRequestHandler<CreateDatabaseCommand>
    {
        private readonly BaldaGameDbContext _context;

        public CreateDataBaseHandler(BaldaGameDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(CreateDatabaseCommand request, CancellationToken cancellationToken)
        {
            await _context.Database.MigrateAsync(cancellationToken);
        }
    }
}