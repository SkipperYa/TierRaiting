using FluentValidation;

namespace Infrastructure.Queries
{
	public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
	{
		public GetWeatherForecastQueryValidator()
		{

		}
	}
}
