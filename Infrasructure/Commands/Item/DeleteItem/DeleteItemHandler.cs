using AutoMapper;
using Infrastructure.Database;
using Domain.Entities;

namespace Infrastructure.Commands
{
	public class DeleteItemHandler : BaseDeleteCommandHandler<DeleteItemCommand, Item>
	{
		public DeleteItemHandler(ApplicationContext applicationContext, IMapper mapper) : base(applicationContext, mapper)
		{
		}
	}
}
