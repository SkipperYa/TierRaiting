using Infrastructure.Queries.WeatherForecastCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IMediator _mediator;

		public WeatherForecastController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
		{
			var query = new GetWeatherForecastQuery();

			var result = await _mediator.Send(query, cancellationToken);

			return result;
		}
	}
}
