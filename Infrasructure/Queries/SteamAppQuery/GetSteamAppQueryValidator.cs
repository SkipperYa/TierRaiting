using FluentValidation;
using Infrastructure.Queries.WeatherForecastCommand;

namespace Infrastructure.Queries
{
	public class GetSteamAppQueryValidator : AbstractValidator<GetSteamAppQuery>
	{
		public GetSteamAppQueryValidator()
		{

		}
	}
}
