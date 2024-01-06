using Infrastructure.BaseValidators;
using Infrastructure.Queries.GetCategoriesQuery;

namespace Infrastructure.Queries.WeatherForecastQuery
{
	public class CategoriesQueryValidator : BaseListQueryValidators<CategoriesQuery>
	{
		public CategoriesQueryValidator() : base()
		{

		}
	}
}
