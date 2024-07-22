using FluentValidation;

namespace Infrastructure.Commands
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(q => q.Email)
				.EmailAddress();

			RuleFor(q => q.Password)
				.NotEmpty()
				.WithMessage("Password is required");
		}
	}
}
