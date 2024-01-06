using FluentValidation;
using Infrastructure.BaseRequest;

namespace Infrastructure.BaseValidators
{
	public abstract class BaseCreateCommandValidator<TCommand> : AbstractValidator<TCommand>
		where TCommand : IBaseAuthorizeRequest
	{
		public BaseCreateCommandValidator()
		{
			RuleFor(q => q.UserId)
				.NotEmpty()
				.NotNull()
				.WithMessage("UserId is required.");
		}
	}

	public abstract class BaseListQueryValidators<TQuery> : AbstractValidator<TQuery>
		where TQuery : IBaseAuthorizeListRequest
	{
		public BaseListQueryValidators()
		{
			RuleFor(q => q.Page)
				.GreaterThan(0)
				.WithMessage("Page must be greater than 0");

			RuleFor(q => q.Count)
				.GreaterThan(0)
				.WithMessage("Page must be greater than 0");
		}
	}
}
