using AutoMapper;
using Infrastructure.Database;
using Domain.Entities;

namespace Infrastructure.Commands
{
	public class UpdateItemHandler : BaseUpdateCommandHandler<UpdateItemCommand, Item>
	{
		public UpdateItemHandler(ApplicationContext applicationContext, IMapper mapper)
			: base(applicationContext, mapper)
		{
		}
	}
}
