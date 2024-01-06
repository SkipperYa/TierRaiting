using Domain.Models;
using Infrastructure.BaseRequest;
using System.Collections.Generic;

namespace Infrastructure.Queries.GetCategoriesQuery
{
	public class CategoriesQuery : BaseAuthorizeListRequest<List<CategoryViewModel>>
	{

	}
}
