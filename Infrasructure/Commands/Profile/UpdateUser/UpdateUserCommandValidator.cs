using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands.Profile
{
	public class UpdateUserCommandValidator : BaseAuthorizeValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator() : base()
		{
			RuleFor(q => q.UserName)
				.NotEmpty()
				.NotNull()
				.WithMessage("UserName is required.");

			RuleFor(q => q.Email)
				.EmailAddress();
		}
	}
}
