using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class UpdateCategoryCommandValidator : BaseUpdateCommandValidator<UpdateCategoryCommand>
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
