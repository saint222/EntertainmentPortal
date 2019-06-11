using EP.Balda.Logic.Models;
using MediatR;

namespace EP.Balda.Logic.Queries
{
    public class GetMap : IRequest<Map>
    {
        public long Id { get; set; }
    }
}