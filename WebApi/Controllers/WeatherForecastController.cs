using Infrastructure.Queries.WeatherForecastCommand;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class WeatherForecastController : BaseApplicationController
	{
		public WeatherForecastController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
		{
			var userId = UserId;

			var query = new GetWeatherForecastQuery(userId);

			var result = await _mediator.Send(query, cancellationToken);

			return result;
		}
	}
}
