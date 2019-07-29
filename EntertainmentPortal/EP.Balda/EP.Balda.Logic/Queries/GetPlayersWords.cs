using System.Collections.Generic;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetPlayersOpponentWords : IRequest<List<string>>
    {
        public long GameId { get; set; }
        public string PlayerId { get; set; }
    }
}
