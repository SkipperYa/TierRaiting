using FluentValidation;

namespace Infrastructure.Commands.CreateCategory
{
	public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryCommandValidator()
		{
			RuleFor(q => q.Title)
				.NotEmpty()
				.NotNull()
				.WithMessage("Title is required.");
		}
	}
}
