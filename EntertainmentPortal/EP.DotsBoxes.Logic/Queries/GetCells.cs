using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;
using System.Collections.Generic;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetCells : IRequest<Maybe<IEnumerable<Cell>>>
    {
    }
}
