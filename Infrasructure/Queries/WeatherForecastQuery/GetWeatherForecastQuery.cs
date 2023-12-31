using MediatR;
using System.Collections.Generic;
using WebApi;

namespace Infrastructure.Queries.WeatherForecastCommand
{
	public class GetWeatherForecastQuery : IRequest<List<WeatherForecast>>
	{

	}
}
