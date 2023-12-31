using Domain.Entities;
using Infrastructure.Database;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
	public abstract class BaseCreateCommand<TCommand, TResult> : IRequestHandler<TCommand, TResult>
		where TCommand: IRequest<TResult>
		where TResult : WithId
	{
		protected readonly ApplicationContext _applicationContext;

		public BaseCreateCommand(ApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken) => 
			await HandleAsync(request, cancellationToken);

		public abstract Task<TResult> HandleAsync(TCommand request, CancellationToken cancellationToken);
	}
}
