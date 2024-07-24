using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class UpdateItemCommandValidator : BaseIdCommandValidator<UpdateItemCommand>
	{
		public UpdateItemCommandValidator() : base()
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
