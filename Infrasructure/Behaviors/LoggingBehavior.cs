using Domain.Exceptions;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
		: IRequest<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			using var log = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.File($"logs/log.txt", rollingInterval: RollingInterval.Day)
				.WriteTo.Console()
				.CreateLogger();

			try
			{
				return await next();
			}
			catch (Exception e)
			{
				var requestName = typeof(TRequest).Name;

				log.Error(e, requestName);
				throw new LogicException(e.ToString());
			}
			finally
			{
				await Log.CloseAndFlushAsync();
			}
		}
	}
}
