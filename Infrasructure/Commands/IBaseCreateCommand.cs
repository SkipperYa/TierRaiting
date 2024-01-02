using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
	public interface IBaseCreateCommand<TRequest, TResult>
	{
		Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
		Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
	}
}