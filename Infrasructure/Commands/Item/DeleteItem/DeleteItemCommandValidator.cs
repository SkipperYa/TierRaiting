using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class DeleteItemCommandValidator : BaseDeleteCommandValidator<DeleteItemCommand>
	{
		public DeleteItemCommandValidator() : base()
		{

		}
	}
}
