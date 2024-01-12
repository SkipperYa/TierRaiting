using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class DeleteCategoryCommandValidator : BaseDeleteCommandValidator<DeleteCategoryCommand>
	{
		public DeleteCategoryCommandValidator() : base()
		{

		}
	}
}
