using FluentValidation;
using Infrastructure.BaseValidators;

namespace Infrastructure.Queries
{
	public class GetItemsQueryValidator : BaseListQueryValidators<GetItemsQuery>
	{
		public GetItemsQueryValidator() : base()
		{
			RuleFor(q => q.CategoryId)
				.NotNull()
				.NotEmpty()
				.WithMessage("CategoryId is required");
		}
	}
}
