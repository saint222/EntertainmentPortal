using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EP.DotsBoxes.Data.Context;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Handlers
{
    public class AddNewPLayerHandler : IRequestHandler<AddNewPlayerCommand,Player>
    {
        private readonly PlayerDbContext _context;
        private readonly IMapper _mapper;

        public AddNewPLayerHandler(PlayerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Player> Handle(AddNewPlayerCommand request, CancellationToken cancellationToken)
        {
            var model = new PlayerDb
            {
                Name = request.Name,
                Color = request.Color,
                Score = request.Score
            };

            _context.Players.Add(model);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return _mapper.Map<Player>(model);
        }
    }
}
