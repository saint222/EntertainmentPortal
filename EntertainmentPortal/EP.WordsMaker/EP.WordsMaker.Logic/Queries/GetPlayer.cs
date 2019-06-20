using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Queries
{
	public class GetPlayer : IRequest<Result<Player>>
	{
		
	}
}