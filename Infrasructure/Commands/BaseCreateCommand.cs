using Infrastructure.BaseRequest;
using Infrastructure.Database;

namespace Infrastructure.Commands
{
	public abstract class BaseCreateCommand<TRequest, TResult> : BaseAuthorizeHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
	{
		protected readonly ApplicationContext _applicationContext;

		public BaseCreateCommand(ApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}
	}
}
