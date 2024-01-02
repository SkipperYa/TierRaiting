using Domain.Exceptions;
using FluentValidation;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IValidator<TRequest> _validator;

		public ValidationBehavior(IValidator<TRequest> validator)
		{
			_validator = validator;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var validationResult = await _validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				var stringBuilder = new StringBuilder();

				foreach (var error in validationResult.Errors)
				{
					stringBuilder.Append(error.ErrorMessage);
					stringBuilder.Append('\n');
				}

				throw new LogicException(stringBuilder.ToString());
			}

			return await next();
		}
	}
}
