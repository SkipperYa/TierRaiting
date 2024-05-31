using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Commands.Profile.BanUser
{
	public class BanUserCommandValidation : BaseAuthorizeValidator<BanUserCommand>
	{
		public BanUserCommandValidation()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.WithMessage("Id is required");
		}
	}
}
