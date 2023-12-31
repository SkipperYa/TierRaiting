using Infrastructure.Queries.WeatherForecastCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	public class WeatherForecastController : BaseApplicationController
	{
		public WeatherForecastController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
		{
			var query = new GetWeatherForecastQuery();

			var result = await _mediator.Send(query, cancellationToken);
			throw new Exception("Biba");
			return result;
		}
	}
}
