using AutoMapper;
using Domain.Entities;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
	public abstract class BaseSaveCommandHandler<TRequest, TResult> : BaseAuthorizeHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
		where TResult : WithId
	{
		protected readonly ApplicationContext _applicationContext;
		protected readonly IMapper _mapper;
		protected virtual EntityState EntityState => EntityState.Added;

		public BaseSaveCommandHandler(ApplicationContext applicationContext, IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		protected virtual async Task<TResult> Save(TRequest request, CancellationToken cancellationToken)
		{
			var entity = _mapper.Map<TResult>(request);

			var item = await _applicationContext.AddAsync(entity, cancellationToken);

			item.State = EntityState;

			await _applicationContext.SaveChangesAsync(cancellationToken);

			return entity;
		}

		protected virtual Task<TRequest> BeforeSave(TRequest request, CancellationToken cancellationToken) 
			=> Task.FromResult(request);

		protected virtual Task<TResult> AfterSave(TResult result, TRequest request, CancellationToken cancellationToken)
			=> Task.FromResult(result);

		public override async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
		{
			return await AfterSave(await Save(await BeforeSave(request, cancellationToken), cancellationToken), request, cancellationToken);
		}
	}
}
