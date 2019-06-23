using System.Collections.Generic;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Queries
{
	public class GetAllGames : IRequest<Maybe<IEnumerable<GameDb>>>
	{
	}
}