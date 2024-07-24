using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class DeleteCategoryCommandValidator : BaseIdCommandValidator<DeleteCategoryCommand>
	{
		public DeleteCategoryCommandValidator() : base()
		{

		}
	}
}
