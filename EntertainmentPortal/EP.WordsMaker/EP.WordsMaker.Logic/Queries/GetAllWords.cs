using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Models;
using MediatR;
namespace EP.WordsMaker.Logic.Queries
{
    public class GetAllWords : IRequest<Maybe<IEnumerable<WordDb>>>
    {
    }
}
