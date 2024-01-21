using Infrastructure.BaseRequest;
using System.Collections.Generic;
using WebApi;

namespace Infrastructure.Queries
{
	public class GetWeatherForecastQuery : BaseAuthorizeRequest<List<WeatherForecast>>
	{
	}
}
