using FluentValidation;

namespace Infrastructure.Commands.CreateCategory
{
	public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
	{
		public CreateCategoryValidator()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.NotNull()
				.WithMessage("Id is required.");

			RuleFor(q => q.Title)
				.NotEmpty()
				.NotNull()
				.WithMessage("Title is required.");
		}
	}
}
