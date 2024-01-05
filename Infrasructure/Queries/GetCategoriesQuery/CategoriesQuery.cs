using Infrastructure.BaseRequest;
using System.Collections.Generic;
using WebApi.Entities;

namespace Infrastructure.Queries.GetCategoriesQuery
{
	public class CategoriesQuery : BaseAuthorizeListRequest<List<Category>>
	{

	}
}
