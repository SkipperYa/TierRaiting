using Domain.Models;
using Infrastructure.BaseRequest;
using System.Collections.Generic;

namespace Infrastructure.Queries
{
	public class GetItemsQuery : BaseAuthorizeListRequest<List<ItemViewModel>>
	{
		public string CategoryId { get; set; }
	}
}
