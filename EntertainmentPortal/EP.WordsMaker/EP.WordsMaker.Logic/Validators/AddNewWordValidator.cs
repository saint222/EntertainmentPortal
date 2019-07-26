using EP.WordsMaker.Logic.Commands;
using FluentValidation;

namespace EP.WordsMaker.Logic.Validators
{
	public class AddNewWordValidator  :AbstractValidator<AddNewWordCommand>
	{

		public AddNewWordValidator()
		{
			RuleFor(x => x.GameId)
				.NotEmpty()
				.WithMessage("Title cannot be null");
		}
	}
}