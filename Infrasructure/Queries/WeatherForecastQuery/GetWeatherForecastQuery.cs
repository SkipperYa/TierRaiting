using Infrastructure.BaseRequest;
using System.Collections.Generic;
using WebApi;

namespace Infrastructure.Queries.WeatherForecastCommand
{
	public class GetWeatherForecastQuery : BaseAuthorizeRequest<List<WeatherForecast>>
	{
		public GetWeatherForecastQuery(string userId) : base(userId)
		{
		}
	}
}
