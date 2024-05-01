using Infrastructure.BaseValidators;

namespace Infrastructure.Queries
{
	public class GetDashboardQueryValidator : BaseAuthorizeValidator<GetDashboardQuery>
	{
		public GetDashboardQueryValidator() : base()
		{

		}
	}
}
