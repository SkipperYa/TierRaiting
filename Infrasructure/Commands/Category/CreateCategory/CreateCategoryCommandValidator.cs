using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class CreateCategoryCommandValidator : BaseCreateCommandValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator() : base()
		{
			RuleFor(q => q.Title)
				.NotEmpty()
				.NotNull()
				.WithMessage("Title is required.");
		}
	}
}
