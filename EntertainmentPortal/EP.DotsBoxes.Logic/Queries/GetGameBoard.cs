using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.DotsBoxes.Data.Models;
using EP.DotsBoxes.Logic.Models;
using MediatR;

namespace EP.DotsBoxes.Logic.Queries
{
    public class GetGameBoard : IRequest<Maybe<IEnumerable<CellDb>>>
    {

    }
}
