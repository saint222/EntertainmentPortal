using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
	public class AddNewGameCommand:IRequest<Result<Game>>
	{
		public string PlayerId { get; set; }
	}
}