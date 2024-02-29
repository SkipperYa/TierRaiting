using Domain.Interfaces;
using FluentValidation;
using Infrastructure.BaseRequest;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

	public abstract class BaseUpdateCommandValidator<TCommand> : AbstractValidator<TCommand>
		where TCommand : IBaseAuthorizeRequest, IWithId
	{
		public BaseUpdateCommandValidator()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.NotNull()
				.WithMessage("Id is required.");

			RuleFor(q => q.UserId)
				.NotEmpty()
				.NotNull()
				.WithMessage("UserId is required.");
		}
	}

	public abstract class BaseDeleteCommandValidator<TCommand> : AbstractValidator<TCommand>
		where TCommand : IBaseAuthorizeRequest, IWithId
	{
		public BaseDeleteCommandValidator()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.NotNull()
				.WithMessage("Id is required.");

			RuleFor(q => q.UserId)
				.NotEmpty()
				.NotNull()
				.WithMessage("UserId is required.");
		}
	}

	public abstract class BaseGetQueryValidator<TQuery> : AbstractValidator<TQuery>
		where TQuery : IBaseGetAuthorizeRequest
	{
		public BaseGetQueryValidator()
		{
			RuleFor(q => q.Id)
				.NotEmpty()
				.NotNull()
				.WithMessage("Id is required.");

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
