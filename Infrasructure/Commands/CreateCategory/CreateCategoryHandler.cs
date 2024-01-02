using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class CreateCategoryHandler : BaseCreateCommand<CreateCategoryCommand, Category>
	{
		public CreateCategoryHandler(ApplicationContext applicationContext) : base(applicationContext)
		{
		}

		public override async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = new Category()
			{
				Id = request.Id,
				Title = request.Title,
				Description = request.Description,
				UserId = request.UserId,
			};

			var item = await _applicationContext.AddAsync(category, cancellationToken);

			item.State = EntityState.Added;

			await _applicationContext.SaveChangesAsync(cancellationToken);

			return category;
		}
	}
}
