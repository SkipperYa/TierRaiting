using AutoMapper;
using Infrastructure.Database;
using WebApi.Entities;

namespace Infrastructure.Commands
{
	public class CreateCategoryHandler : BaseCreateCommandHandler<CreateCategoryCommand, Category>
	{
		public CreateCategoryHandler(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
		{
		}
	}
}
