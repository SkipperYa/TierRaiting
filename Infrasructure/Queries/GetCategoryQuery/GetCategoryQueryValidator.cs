using Infrastructure.BaseValidators;

namespace Infrastructure.Queries
{
	public class GetCategoryQueryValidator : BaseAuthorizeValidator<GetCategoryQuery>
	{
		public GetCategoryQueryValidator() : base()
		{

		}
	}
}
