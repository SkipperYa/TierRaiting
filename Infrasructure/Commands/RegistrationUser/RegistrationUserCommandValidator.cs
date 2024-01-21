using FluentValidation;

namespace Infrastructure.Commands
{
	public class RegistrationUserCommandValidator : AbstractValidator<RegistrationUserCommand>
	{
		public RegistrationUserCommandValidator()
		{
			RuleFor(q => q.Email)
				.EmailAddress();

			RuleFor(q => q.UserName)
				.NotEmpty()
				.WithMessage("User Name is required");

			RuleFor(q => q.Password)
				.NotEmpty()
				.WithMessage("Password is required.");

			RuleFor(q => q.PasswordConfirm)
				.Equal(q => q.Password)
				.WithMessage("Password Confirm must equal Password.");
		}
	}
}
