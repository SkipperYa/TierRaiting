using Domain.Interfaces;
using FluentValidation;
using Infrastructure.BaseRequest;

namespace Infrastructure.BaseValidators
{
	public abstract class BaseAuthorizeValidator<TCommand> : AbstractValidator<TCommand>
		where TCommand : IBaseAuthorizeRequest
	{
		public BaseAuthorizeValidator()
		{
			RuleFor(q => q.UserId)
				.NotEmpty()
				.NotNull()
				.WithMessage("UserId is required.");
		}
	}

	public abstract class BaseIdCommandValidator<TCommand> : BaseAuthorizeValidator<TCommand>
		where TCommand : IBaseAuthorizeRequest, IWithId
	{
		public BaseIdCommandValidator() : base()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.NotNull();
		}
	}

	public abstract class BaseGetQueryValidator<TQuery> : BaseAuthorizeValidator<TQuery>
		where TQuery : IBaseGetAuthorizeRequest
	{
		public BaseGetQueryValidator() : base()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.NotNull()
				.WithMessage("Id is required.");
		}
	}

	public abstract class BaseListQueryValidators<TQuery> : BaseAuthorizeValidator<TQuery>
		where TQuery : IBaseAuthorizeListRequest
	{
		public BaseListQueryValidators() : base()
		{
			RuleFor(q => q.Page)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Page must be greater than 0");

			RuleFor(q => q.Count)
				.GreaterThan(0)
				.WithMessage("Page must be greater than 0");
		}
	}
}
