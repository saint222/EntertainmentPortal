using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
	public class GetGameCommand : IRequest<Result<Game>>
	{
		public int Id { get; set; }
	}
}