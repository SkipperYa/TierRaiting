using AutoMapper;
using Infrastructure.Database;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Infrastructure.Commands
{
	public class UpdateCategoryHandler : BaseUpdateCommandHandler<UpdateCategoryCommand, Category>
	{
		public UpdateCategoryHandler(ApplicationContext applicationContext, IMapper mapper)
			: base(applicationContext, mapper)
		{

		}
	}
}
