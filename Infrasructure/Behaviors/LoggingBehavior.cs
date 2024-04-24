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
			try
			{
				return await next();
			}
			catch (Exception e)
			{
				Log.Error(e, typeof(TRequest).Name);

				await Log.CloseAndFlushAsync();

				// Throw exception again for send it to client
				throw new LogicException(e.Message);
			}
		}
	}
}
