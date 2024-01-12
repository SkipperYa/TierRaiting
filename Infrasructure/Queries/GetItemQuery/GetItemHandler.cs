using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using Domain.Entities;

namespace Infrastructure.Queries
{
	public class GetItemHandler : BaseGetQueryHandler<GetItemQuery, Item, ItemViewModel>
	{
		public GetItemHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}
	}
}
