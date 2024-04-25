using FluentValidation;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class SendConfirmCommandValidator : AbstractValidator<SendConfirmCommand>
	{
		public SendConfirmCommandValidator()
		{
			RuleFor(q => q.UserId)
				.NotEmpty()
				.WithMessage("UserId is required");
		}
	}
}
