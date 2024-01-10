using AutoMapper;
using Infrastructure.Database;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class UpdateCategoryHandler : BaseUpdateCommandHandler<UpdateCategoryCommand, Category>
	{
		public UpdateCategoryHandler(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
		{
		}
	}
}
