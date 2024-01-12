using AutoMapper;
using Infrastructure.Database;
using Domain.Entities;

namespace Infrastructure.Commands
{
	public class CreateItemHandler : BaseCreateCommandHandler<CreateItemCommand, Item>
	{
		public CreateItemHandler(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
		{
		}
	}
}
