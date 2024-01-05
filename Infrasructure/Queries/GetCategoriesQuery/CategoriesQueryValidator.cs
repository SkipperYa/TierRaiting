using FluentValidation;
using Infrastructure.Queries.GetCategoriesQuery;

namespace Infrastructure.Queries.WeatherForecastQuery
{
	public class CategoriesQueryValidator : AbstractValidator<CategoriesQuery>
	{
		public CategoriesQueryValidator()
		{
			RuleFor(q => q.Page)
				.GreaterThan(0)
				.WithMessage("Page must be greater than 0");
		}
	}
}
