using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands
{
	public class DeleteItemCommandValidator : BaseIdCommandValidator<DeleteItemCommand>
	{
		public DeleteItemCommandValidator() : base()
		{

		}
	}
}
