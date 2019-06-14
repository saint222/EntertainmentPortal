using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Models;
using MediatR;
namespace EP.WordsMaker.Logic.Queries
{
    public class GetAllPlayers : IRequest<Maybe<IEnumerable<PlayerDb>>>
    {
    }
}
