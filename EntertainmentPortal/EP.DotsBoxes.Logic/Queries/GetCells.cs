using CSharpFunctionalExtensions;
using EP.DotsBoxes.Logic.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetCells : IRequest<Maybe<IEnumerable<Cell>>>
    {
    }
}
