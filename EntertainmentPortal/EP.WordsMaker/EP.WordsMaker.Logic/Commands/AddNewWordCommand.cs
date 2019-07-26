using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
	public class AddNewWordCommand:IRequest<Result<Word>>
	{
		public string GameId { get; set; }
		public string Value { get; set; }
	}
}