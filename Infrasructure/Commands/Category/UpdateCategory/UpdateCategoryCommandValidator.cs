using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class UpdateCategoryCommandValidator : BaseIdCommandValidator<UpdateCategoryCommand>
	{
		public UpdateCategoryCommandValidator() : base()
		{
			RuleFor(q => q.Title)
				.NotEmpty()
				.NotNull()
				.WithMessage("Title is required.");
		}
	}
}
