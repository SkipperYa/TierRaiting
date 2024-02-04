using Domain.Entities;
using Domain.Models;
using Infrastructure.BaseRequest;
using System;

namespace Infrastructure.Queries
{
	public class GetItemsQuery : BaseAuthorizeListRequest<PagedList<ItemViewModel>>
	{
		public Guid CategoryId { get; set; }
	}
}
