using FluentValidation;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class ConfirmUserCommandValidator : AbstractValidator<ConfirmUserCommand>
	{
		public ConfirmUserCommandValidator()
		{
			RuleFor(q => q.UserId)
				.NotEmpty()
				.WithMessage("UserId is required");

			RuleFor(q => q.Token)
				.NotEmpty()
				.WithMessage("Token is required.");

			RuleFor(q => q.Email)
				.EmailAddress()
				.When(q => !string.IsNullOrEmpty(q.Email))
				.WithMessage("Invalid Email");
		}
	}
}
