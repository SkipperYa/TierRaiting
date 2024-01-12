using AutoMapper;
using Infrastructure.Database;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class DeleteCategoryHandler : BaseDeleteCommandHandler<DeleteCategoryCommand, Category>
	{
		public DeleteCategoryHandler(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
		{
		}
	}
}
