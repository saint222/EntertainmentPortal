using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Models;
using MediatR;

namespace EP.WordsMaker.Logic.Commands
{
	public class GetAllWordsCommand : IRequest<Result<IEnumerable<Word>>>
	{
		public string GameId { get; set; }
	}
}