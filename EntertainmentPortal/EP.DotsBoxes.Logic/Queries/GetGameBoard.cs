using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetGameBoard : IRequest<Maybe<IEnumerable<CellDb>>>
    {

    }
}
