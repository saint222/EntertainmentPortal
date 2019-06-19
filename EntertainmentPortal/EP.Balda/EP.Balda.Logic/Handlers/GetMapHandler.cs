using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Models;
using EP.Balda.Logic.Queries;
using MediatR;

namespace EP.Balda.Logic.Handlers
{
    public class GetMapHandler : IRequestHandler<GetMap, Maybe<MapDb>>
    {
        public Task<Maybe<MapDb>> Handle(GetMap request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
