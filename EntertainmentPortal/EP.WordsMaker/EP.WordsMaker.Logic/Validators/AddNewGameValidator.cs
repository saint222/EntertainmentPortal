using EP.WordsMaker.Logic.Commands;
using FluentValidation;

namespace EP.WordsMaker.Logic.Validators
{
	public class AddNewGameValidator  :AbstractValidator<AddNewGameCommand>
	{

		public AddNewGameValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.WithMessage("Title cannot be null");
		}
	}
}