using CSharpFunctionalExtensions;
using EP.WordsMaker.Data.Models;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
	public class GetPlayerCommand : IRequest<Result<Player>>
	{
		public int Id { get; set; }
	}
}