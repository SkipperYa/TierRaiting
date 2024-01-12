using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class CreateItemCommandValidator : BaseCreateCommandValidator<CreateItemCommand>
	{
		public CreateItemCommandValidator() : base()
		{
			RuleFor(q => q.Title)
				.NotEmpty()
				.NotNull()
				.WithMessage("Title is required.");

			RuleFor(q => q.CategoryId)
				.NotEmpty()
				.NotNull()
				.WithMessage("CategoryId is required.");
		}
	}
}
