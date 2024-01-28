using Domain.Entities;
using Domain.Models;
using Infrastructure.BaseRequest;

namespace Infrastructure.Queries
{
	public class CategoriesQuery : BaseAuthorizeListRequest<PagedList<CategoryViewModel>>
	{

	}
}
