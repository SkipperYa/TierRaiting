using Domain.Entities;
using Domain.Models;
using Infrastructure.BaseRequest;

namespace Infrastructure.Queries
{
	public class GetItemsQuery : BaseAuthorizeListRequest<PagedList<ItemViewModel>>
	{
		public string CategoryId { get; set; }
	}
}
