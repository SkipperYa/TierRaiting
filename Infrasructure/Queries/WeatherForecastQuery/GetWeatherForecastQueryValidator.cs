using FluentValidation;
using Infrastructure.Queries.WeatherForecastCommand;

namespace Infrastructure.Queries.WeatherForecastQuery
{
	public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
	{
		public GetWeatherForecastQueryValidator()
		{

		}
	}
}
